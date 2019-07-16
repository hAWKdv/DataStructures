import { Node, Tree } from './tree';

export class BinarySearchTree<T> extends Tree<T> {
  add(value: T) {
    const node = new Node(value);
    if (this.root) {
      this._addNewNode(this.root, node);
    } else {
      this.root = node;
    }
  }

  delete(value: T) {

  }

  private _addNewNode(n: Node<T>, newNode: Node<T>) {
    const move = (idx: number) => {
      if (n.children[idx]) {
        this._addNewNode(n.children[idx], newNode);
      } else {
        n.children[idx] = newNode;
      }
    };

    if (newNode.value < n.value) {
      move(0);
    } else {
      move(1);
    }
  }
}

// Demo:

const bst = new BinarySearchTree<number>();
bst.add(3);
bst.add(2);
bst.add(1);
bst.add(5);
bst.add(6);

console.log(bst.toString());
