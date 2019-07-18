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
    const result = this._searchNode(this.root, value);
    if (!result) {
      return;
    }

    const [ node, parent ] = result;
    const childrenNum = node.children.filter(c => !!c).length;

    if (!childrenNum) {
      if (parent.children[0] === node) {
        parent.children[0] = undefined;
      } else {
        parent.children[1] = undefined;
      }
    } else if (childrenNum === 1) {
      const child = node.children[0] || node.children[1];
      node.value = child.value;
      node.children = [];
    } else {
      let swapNodeParent = node;
      let swapNode = node.children[0];

      while (swapNode) {
        if (swapNode.children[0]) {
          swapNodeParent = swapNode;
          swapNode = swapNode.children[0];
        } else if (swapNode.children[1]) {
          swapNodeParent = swapNode;
          swapNode = swapNode.children[1];
        } else {
          break;
        }
      }

      node.value = swapNode.value;
      swapNodeParent.children = swapNodeParent.children.filter(n => n !== swapNode);
    }
  }

  get(value: T): Node<T> | null {
    const result = this._searchNode(this.root, value);
    if (result) {
      return result[0];
    }
    return null;
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

  private _searchNode(n: Node<T>, v: T, parent?: Node<T>): [Node<T>, Node<T>] | null {
    if (n.value === v) {
      return [n, parent];
    }
    const left = n.children[0];
    if (left && v < n.value) {
      return this._searchNode(left, v, n);
    }
    const right = n.children[1];
    if (right) {
      return this._searchNode(right, v, n);
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
        const left = n.children[0];
        const right = n.children[1];
        str += `${n.value} -> ${left ? left.value : '_'}, ${right ? right.value : '_'}\n`;

        n.children.forEach((c: Node<T>) => queue.push(c));
      }
    }

    return str;
  }
}
