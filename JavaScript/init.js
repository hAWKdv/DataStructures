"use strict";

var LinkedList = require("./collections/LinkedList");
var DoublyLinkedList = require("./collections/DoublyLinkedList").DoublyLinkedList;

// NOTE: Test only
var list = new LinkedList();
//list.insert(2, 3, 4);

//list.delete(3);

list.insert(1);

list.delete(1);
list.insert(3, 4, 5);
//console.log(list.exists(8));
//list.empty();

//console.log(list.head.value);
//console.log(list.tail.value);

list.forEach(function (item) {
    console.log(item);
});

console.log(list.toArray());
//
//console.log("----");
//
//console.log(list.getLength());

//var dlist = new DoublyLinkedList();
//
//dlist.insert(1, 2, 3);
//
//dlist.forEach(function(item) {
//    var prev, next;
//
//    if (item.prev) {
//        prev = item.prev.value;
//    }
//
//    if (item.next) {
//        next = item.next.value;
//    }
//
//    console.log("[" + item.value + "] => Prev: " + prev + "; Next: " + next);
//});