"use strict";

var Item = (function() {
    function Item(value) {
        this.value = value;
    }

    Item.prototype.setNext = function(next) {
        if (next instanceof Item) {
            this.next = next;
        }
    };

    return Item;
}());

var LinkedList = (function(Item) {
    var _length = 0;

    function _insertNewItem(value) {
        var newItem;

        if (this.head.value) {
            newItem = new Item(value);

            this.tail.setNext(newItem);
            this.tail = newItem;
        } else {
            this.head.value = value;
        }

        _length += 1;
    }

    function LinkedList(value) {
        this.head = new Item(value);
        this.tail = this.head;

        if (value) {
            _length += 1;
        }
    }

    // Allows insert
    LinkedList.prototype.insert = function() {
        var self,
            args;

        if (arguments.length === 1) {
            _insertNewItem.call(this, arguments[0]);
        } else {
            args = [].slice.call(arguments);
            self = this;

            args.forEach(function (argument) {
                _insertNewItem.call(self, argument);
            });
        }

        return this;
    };

    LinkedList.prototype.delete = function(value) {
        var self = this,
            previous;

        if (_length === 1 && this.head.value === value) {
            this.empty();
            return;
        }

        function traverse(item) {
            if (item.value === value) {
                if (previous) {
                    previous.setNext(item.next);

                    if (!previous.next) {
                        self.tail = previous;
                    }
                } else {
                    self.head = item.next;
                }

                _length -= 1;

                return;
            }

            if (item.next) {
                previous = item;
                traverse(item.next);
            }
        }

        traverse(this.head);
    };

    LinkedList.prototype.exists = function(value) {
        var isExistent = false;

        this.forEach(function(item) {
            if (item.value === value) {
                isExistent = true;
                return true;
            }
        });

        return isExistent;
    };

    // Used to iterate through list.
    LinkedList.prototype.forEach = function(func) {
        function traverse(item) {
            var shouldStop;

            shouldStop = func(item);

            if (item.next && !shouldStop) {
                traverse(item.next);
            }
        }

        traverse(this.head);
    };

    LinkedList.prototype.toArray = function() {
        var array = [];

        this.forEach(function(item) {
            array.push(item.value);
        });

        return array;
    };

    LinkedList.prototype.empty = function() {
        this.head = new Item();
        this.tail = this.head;

        _length = 0;
    };

    // Returns the length of the list
    LinkedList.prototype.getLength = function () {
        return _length;
    };

    return LinkedList;
}(Item));

module.exports = LinkedList;
