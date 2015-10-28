var LinkedList = (function() {
    "use strict";

    var Item = (function() {
        function Item(value) {
            this.value = value;
        }
        
        Item.prototype.setNext = function (next) {
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
    }

    function LinkedList(value) {
        this.head = new Item(value);
        this.tail = this.head;
    }

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
    
    return LinkedList;
}());

// NOTE: Test only
var list = new LinkedList(1);
list.insert(2, 3, 4);

console.log(list.head.value);
console.log(list.head.next.value);
console.log(list.head.next.next.value);
console.log(list.head.next.next.next.value);
