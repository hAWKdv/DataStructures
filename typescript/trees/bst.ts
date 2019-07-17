import { Node, Tree } from './tree';

export class BinarySearchTree<T> extends Tree<T> {
  add(value: T): void {
    const node = new Node(value);
    if (this.root) {
      this._addNewNode(this.root, node);
    } else {
      this.root = node;
    }
  }

  delete(value: T): void {

  }

  has(value: T): boolean {
    return this._searchValue(this.root, value);
  }

  private _addNewNode(n: Node<T>, newNode: Node<T>): void {
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

  private _searchValue(n: Node<T>, v: T): boolean {
    if (n.value === v) {
      return true;
    }
    const left = n.children[0];
    if (left && v < left.value) {
      return this._searchValue(left, v);
    }
    const right = n.children[1];
    if (right) {
      return this._searchValue(right, v);
    }
    return false;
  }

  toString(): string {
    let str = '';
    const queue = [];
    queue.push(this.root);

    while (queue.length) {
      const n = queue.shift();
      const left = n.children[0];
      const right = n.children[1];
      str += `${n.value} -> ${left ? left.value : '_'}, ${right ? right.value : '_'}\n`;

      n.children.forEach((c: Node<T>) => queue.push(c));
    }

    return str;
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

console.log('Is there 9:', bst.has(9));
console.log('Is there 5:', bst.has(5));
