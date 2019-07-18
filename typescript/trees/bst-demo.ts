import { BinarySearchTree } from './bst';

const bst = new BinarySearchTree<number>();
bst.add(5);
bst.add(3);
bst.add(6);
bst.add(5);
bst.add(7);
bst.add(8);
bst.add(7);
bst.add(4);
bst.add(1);

console.log(bst.toString());

console.log('Is there 1:', bst.get(1));
console.log('Is there 5:', !!bst.get(5));
console.log('Is there 9:', bst.get(9));

bst.delete(1);
console.log(bst.toString());

bst.delete(8);
console.log(bst.toString());

bst.delete(6);
console.log(bst.toString());
