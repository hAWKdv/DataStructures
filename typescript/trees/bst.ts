import { Node, Tree } from './tree';

const LeftChildIdx = 0;
const RightChildIdx = 1;

export class BinarySearchTree<T, U = {}> extends Tree<T, U> {
  add(value: T): void {
    const node = new Node<T, U>(value);
    if (this.root) {
      this._addNewNode(this.root, node);
    } else {
      this.root = node;
    }
  }

  delete(value: T): void {
    const result = this._searchNode(this.root, value);
    if (!result) {
      return;
    }

    const node = result;
    const childrenNum = node.children.filter(c => !!c).length;

    if (!childrenNum) {
      const [parentLeftChild] = node.parent.children;

      if (parentLeftChild === node) {
        delete node.parent.children[LeftChildIdx];
      } else {
        delete node.parent.children[RightChildIdx];
      }
    } else if (childrenNum === 1) {
      const [left, right] = node.children;
      const child = left || right;
      node.value = child.value;
      node.children = [];
    } else {
      let swapNodeParent = node;
      let swapNode = node.children[LeftChildIdx];

      while (swapNode) {
        if (swapNode.children[LeftChildIdx]) {
          swapNodeParent = swapNode;
          swapNode = swapNode.children[LeftChildIdx];
        } else if (swapNode.children[RightChildIdx]) {
          swapNodeParent = swapNode;
          swapNode = swapNode.children[RightChildIdx];
        } else {
          break;
        }
      }

      node.value = swapNode.value;
      swapNodeParent.children = swapNodeParent.children.filter(n => n !== swapNode);
    }
  }

  get(value: T): Node<T> | null {
    return this._searchNode(this.root, value);
  }

  protected _addNewNode(n: Node<T>, newNode: Node<T>): void {
    const move = (idx: number) => {
      if (n.children[idx]) {
        this._addNewNode(n.children[idx], newNode);
      } else {
        newNode.parent = n;
        n.children[idx] = newNode;
      }
    };

    if (newNode.value < n.value) {
      move(0);
    } else {
      move(1);
    }
  }

  private _searchNode(n: Node<T>, v: T): Node<T> | null {
    if (n.value === v) {
      return n;
    }
    const [left, right] = n.children;
    if (left && v < n.value) {
      return this._searchNode(left, v);
    }
    if (right) {
      return this._searchNode(right, v);
    }
    return null;
  }

  toString(): string {
    let str = '';
    const queue = [];
    queue.push(this.root);

    while (queue.length) {
      const n = queue.shift();
      if (n) {
        const [left, right] = n.children;
        str += `${n.value} -> ${left ? left.value : '_'}, ${right ? right.value : '_'}\n`;

        n.children.forEach((c: Node<T>) => queue.push(c));
      }
    }

    return str;
  }
}
