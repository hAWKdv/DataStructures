import { Heap } from './heap';

const heap = new Heap();

heap.insert(5);
heap.insert(3);
heap.insert(2);
heap.insert(5);
heap.insert(6);
heap.insert(7);
heap.insert(4);
heap.insert(1);

console.log(heap.extractMin()); // 1
console.log(heap.extractMin()); // 2
console.log(heap.extractMin()); // 3
console.log(heap.extractMin()); // 4
console.log(heap.extractMin()); // 5
