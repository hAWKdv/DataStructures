var linkedListObj = require("./collections/LinkedList"),
    LinkedList = linkedListObj.LinkedList,
    LinkedListItem = linkedListObj.Item;

var Queue = (function(LinkedList, Item) {
    "use strict";

    var _container = new LinkedList(),
        _length = 0;

    function Queue() {}

    Queue.prototype.enqueue = function(value) {
        var enqueued = new Item(value);

        enqueued.next = _container.head;
        _container.head = enqueued;

        _length += 1;
    };

    Queue.prototype.dequeue = function() {
        var dequeued = _container.tail;
    };

    return Queue;
}(LinkedList, LinkedListItem));

module.exports = Queue;
