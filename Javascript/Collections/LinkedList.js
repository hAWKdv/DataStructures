var LinkedList = (function() {
    "use strict";

    var _length = 0,
        Item;

    Item = (function() {
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

    function _insertNewItem(value) {
        var newItem = new Item(value);

        this.tail.setNext(newItem);
        this.tail = newItem;

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

    //LinkedList.prototype.delete = function(value) {
    //    var previous;
    //
    //    function traverse(item) {
    //        if (item.value === value) {
    //            item = item.next;
    //        }
    //
    //        if (item.next) {
    //            traverse(item.next);
    //        }
    //    }
    //
    //    traverse(this.head);
    //};

    // Used to iterate through list.
    LinkedList.prototype.forEach = function(func) {
        function traverse(item) {
            func(item);

            if (item.next) {
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

    // Returns the length of the list
    LinkedList.prototype.getLength = function () {
        return _length;
    };

    return LinkedList;
}());

// NOTE: Test only
var list = new LinkedList(1);
list.insert(2, 3, 4);

list.forEach(function (item) {
    //console.log(item);
});

//list.delete(2);

console.log(list.toArray());

console.log("----");

console.log(list.getLength());