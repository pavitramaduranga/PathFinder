﻿document.addEventListener('DOMContentLoaded', function () {
    fetchAvailableNodes();
});

function fetchAvailableNodes() {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/api/path/getNodeList', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var nodes = JSON.parse(xhr.responseText);
            displayAvailableNodes(nodes);
        } else if (xhr.readyState == 4 && xhr.status != 200) {
            console.error('Error fetching available nodes');
        }
    };
    xhr.send();
}

function displayAvailableNodes(nodes) {

    var nodesLabel = document.getElementById('availableNodes');
    nodesLabel.textContent = 'Available Nodes: ' + nodes.join(', ');

    var fromNodeSelect = document.getElementById('fromNode');
    var toNodeSelect = document.getElementById('toNode');

    // Clear existing options
    fromNodeSelect.innerHTML = '';
    toNodeSelect.innerHTML = '';

    // Add an empty option (optional)
    fromNodeSelect.add(new Option('Select a Node', ''));
    toNodeSelect.add(new Option('Select a Node', ''));

    // Populate dropdowns with node names
    nodes.forEach(function (node) {
        fromNodeSelect.add(new Option(node, node));
        toNodeSelect.add(new Option(node, node));
    });
}

document.getElementById('learnMoreBtn').addEventListener('click', function (event) {
    clearResponse();
    if (!validateNodes()) {
        event.preventDefault();
    }
    else {
        var fromNode = document.getElementById('fromNode').value;
        var toNode = document.getElementById('toNode').value;

        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/path?from=' + fromNode + '&to=' + toNode, true);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var responseData = JSON.parse(xhr.responseText);
                document.getElementById('successResponseDiv').style.display = 'block';
                document.getElementById('errorAlert').style.display = 'none';
                $('<div>', { text: formatInitialNodeData(fromNode, toNode) }).appendTo($('#initialNodes'));
                $('<div>', { text: formatPathData(responseData) }).appendTo($('#bestPath'));
                $('<div>', { text: formatDistanceData(responseData) }).appendTo($('#distance'));

                console.log(xhr.responseText);
            }
            else {
                document.getElementById('successResponseDiv').style.display = 'none';
                var errorAlert = document.getElementById('errorAlert');
                errorAlert.innerHTML = 'Error occured while calculating the best path.';
                errorAlert.style.display = 'block';
                return false;
            }
        };
        xhr.send();
    }
});

function formatInitialNodeData(fromNode, toNode) {
    return 'Start Node ' + fromNode + ' and End Node ' + toNode;
}

function formatPathData(data) {
    return 'Best Path : ' + data.nodeNames.join(', ');
}

function formatDistanceData(data) {
    return 'Shortest Distance : ' + data.distance;
}

function validateNodes() {
    var fromNode = document.getElementById('fromNode').value;
    var toNode = document.getElementById('toNode').value;
    var errorAlert = document.getElementById('errorAlert');

    var letterRegex = /^[A-Za-z]$/;

    if (fromNode.match(letterRegex) && toNode.match(letterRegex)) {
        errorAlert.style.display = 'none';
        return true;
    } else {
        errorAlert.innerHTML = 'Select both From Node and To Node.';
        errorAlert.style.display = 'block';
        return false;
    }
}

function clearResponse() {
    document.getElementById('errorAlert').style.display = 'none';
    document.getElementById('successResponseDiv').style.display = 'none';
    document.getElementById("initialNodes").innerHTML = "";
    document.getElementById("bestPath").innerHTML = "";
    document.getElementById("distance").innerHTML = "";
}