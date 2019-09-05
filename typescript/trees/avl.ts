import { BinarySearchTree } from './bst';
import { Node } from './tree';

interface NodeMetadata {
  height: number;
  balance: number;
}

// Doesn't work properly
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

    this._updateMetadataFromLeaf(node);
    this._balance(node);
  }

  private _updateMetadataFromLeaf(node: Node<T, NodeMetadata>): void {
    let currParent = node.parent;

    while (currParent) {
      this._recalculateMetadata(currParent);
      currParent = currParent.parent;
    }
  }

  remove(value: T): void {
    throw new Error('Method not implemented');
  }

  // Needs to be fixed
  private _updateMetadataFromSubtree(node: Node<T, NodeMetadata>): void {
    const stack = [node];

    while (stack.length) {
      const curr = stack.pop();

      if (curr.children.length) {
        curr.children.forEach((n: Node<T, NodeMetadata>) => stack.push(n));
      } else {
        this._updateMetadataFromLeaf(curr);
      }
    }
  }

  private _balance(node: Node<T, NodeMetadata>): void {
    if (!node) {
      return;
    }

    const { balance } = node.metadata;
    const [left, right] = node.children;

    if (balance > 1) {
      if (right.metadata.balance > 0) { // Case I: Path-like structure (i.e. left rotation)
        this._leftRotate(node);
        return;
      } else { // Case II: Zig-zag-like structure (i.e. right + left rotation)
        this._rightRotate(right);
        this._leftRotate(node);
      }
    } else if (balance < -1) { // Case I: Path-like structure (i.e. right rotation)
      if (left.metadata.balance < 0) {
        this._rightRotate(node);
      } else { // Case II: Zig-zag-like structure (i.e. left + right rotation)
        this._leftRotate(left);
        this._rightRotate(node);
      }
    }

    this._balance(node.parent);
  }

  private _leftRotate(node: Node<T, NodeMetadata>) {
    const root = node.parent;
    const [_, right] = node.children;
    const rightLeft = right ? right.children[0] : null;

    if (rightLeft) {
      node.children[1] = rightLeft;
      rightLeft.parent = node;
    } else {
      delete node.children[1];
    }

    if (root) {
      const nodeIdx = root.children.indexOf(node);
      root.children[nodeIdx] = right;
    } else {
      this.root = right;
    }
    right.parent = root;

    right.children[0] = node;
    node.parent = right;

    this._updateMetadataFromSubtree(right);
  }

  private _rightRotate(node: Node<T, NodeMetadata>) {
    const root = node.parent;
    const [left] = node.children;
    const leftRight = left ? left.children[1] : null;

    if (leftRight) {
      node.children[0] = leftRight;
      leftRight.parent = node;
    } else {
      delete node.children[0];
    }

    if (root) {
      const nodeIdx = root.children.indexOf(node);
      root.children[nodeIdx] = left;
    } else {
      this.root = left;
    }
    left.parent = root;

    left.children[1] = node;
    node.parent = left;

    this._updateMetadataFromSubtree(left);
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
        str += `${n.value} (H: ${height}, B: ${balance}, P: ${n.parent ? n.parent.value : '*'}) -> ${left ? left.value : '_'}, ${right ? right.value : '_'}\n`;

        n.children.forEach((c: Node<T>) => queue.push(c));
      }
    }

    return str;
  }
}
