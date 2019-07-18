import { BinarySearchTree } from '../bst';
import { Node } from '../tree';

function preorderTraversal<T>(n: Node<T> | undefined, cb: (n: Node<T>) => void): void {
  if (!n) {
    return;
  }

  cb(n);

  preorderTraversal(n.children[0], cb);
  preorderTraversal(n.children[1], cb);
}

function inorderTraversal<T>(n: Node<T> | undefined, cb: (n: Node<T>) => void): void {
  if (!n) {
    return;
  }

  inorderTraversal(n.children[0], cb);
  cb(n);
  inorderTraversal(n.children[1], cb);
}

function postorderTraversal<T>(n: Node<T> | undefined, cb: (n: Node<T>) => void): void {
  if (!n) {
    return;
  }

  postorderTraversal(n.children[0], cb);
  postorderTraversal(n.children[1], cb);

  cb(n);
}

// Demo:

const bst = new BinarySearchTree<number>();

bst.add(5);
bst.add(6);
bst.add(3);
bst.add(4);
bst.add(2);

console.log(bst.toString());

const cb = <T>(n: Node<T>) => n ? console.log(n.value) : 0;

console.log('Preorder: ');
preorderTraversal(bst.root, cb);

console.log('Inorder: ');
inorderTraversal(bst.root, cb);

console.log('Postorder: ');
postorderTraversal(bst.root, cb);
