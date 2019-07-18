import { AVL } from '../trees/avl';

const avl = new AVL<number>();
avl.add(5);
avl.add(3);
avl.add(6);
avl.add(5);
avl.add(7);
avl.add(8);
avl.add(7);
avl.add(4);
avl.add(1);

console.log(avl.toString());
