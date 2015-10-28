var LinkedList = (function() {
    "use strict";

    var Item = (function() {
        function Item(value, next) {
            this.value = value;

            if (next) {
                this.setNext(next);
            }
        }
        
        Item.prototype.setNext = function (next) {
            if (next instanceof Item) {
                this.next = next;
            }
        };

        return Item;
    }());

    function LinkedList(value) {
        this.head = new Item(value);
        this.tail = this.head;
    }

    LinkedList.prototype.insert = function(value) {

    };

    return LinkedList;
}());