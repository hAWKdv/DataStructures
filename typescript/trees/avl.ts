import { BinarySearchTree } from './bst';
import { Node } from './tree';

interface NodeMetadata {
  height: number;
  balance: number;
}

export class AVL<T> extends BinarySearchTree<T, NodeMetadata> {
  add(value: T): void {
    const node = new Node<T, NodeMetadata>(value, {
      height: 0,
      balance: 0,
    });

    if (this.root) {
      this._addNewNode(this.root, node);
    } else {
      this.root = node;
    }

    this._updateMetadata(node);
    this._fixBalance(node);
  }

  private _updateMetadata(node: Node<T, NodeMetadata>): void {
    let currParent = node.parent;

    while (currParent) {
      this._recalculateMetadata(currParent);
      currParent = currParent.parent;
    }
  }

  private _fixBalance(node: Node<T, NodeMetadata>): void {
    if (!node.parent) {
      return;
    }

    const parentBalance = node.parent.metadata.balance;

    if (parentBalance < -1 && 1 < parentBalance) {
      const { balance } = node.metadata;

      // Case I: Path-like structure (i.e. left rotation)
      if (parentBalance < 0 && balance < 0 || parentBalance > 0 && balance > 0) {
        // todo
      } else { // Case II: Zig-zag-like structure (i.e. right + left rotation)
        // todo
      }

      this._fixBalance(node.parent);
    }
  }

  private _recalculateMetadata(node: Node<T, NodeMetadata>): void {
    const [left, right] = node.children;
    const leftHeight = left ? left.metadata.height : -1;
    const rightHeight = right ? right.metadata.height : -1;

    const height = Math.max(leftHeight, rightHeight) + 1;
    const balance = rightHeight - leftHeight;

    node.metadata.height = height;
    node.metadata.balance = balance;
  }


  toString(): string {
    let str = '';
    const queue = [];
    queue.push(this.root);

    while (queue.length) {
      const n = queue.shift();
      if (n) {
        const [left, right] = n.children;
        const { height, balance } = n.metadata;
        str += `${n.value} (H: ${height}, B: ${balance}) -> ${left ? left.value : '_'}, ${right ? right.value : '_'}\n`;

        n.children.forEach((c: Node<T>) => queue.push(c));
      }
    }

    return str;
  }
}
