// Array-less implementation since array implementation is trivial

class Item<T> {
  next: Item<T> | undefined;

  constructor(public value: T) {}
}

export class Queue<T> {
  private _root: Item<T> | undefined;
  private _tip: Item<T> | undefined;

  que(value: T): void {
    const newValue = new Item<T>(value);
    if (this._root) {
      this._tip.next = newValue;
    } else {
      this._root = newValue;
    }
    this._tip = newValue;
  }

  deque(): T | undefined {
    if (!this._root) {
      return;
    }

    const val = this._root.value;
    this._root = this._root.next;

    return val;
  }

  toString(): string {
    let str = '';
    let curr = this._root;

    while (curr) {
      str += curr.value;
      if (curr.next) {
        str += ', ';
      }
      curr = curr.next;
    }

    return `[${str}]`;
  }
}

// Demo:

const queue = new Queue<number>();

queue.que(1);
queue.que(2);
queue.que(3);
queue.que(4);
queue.que(5);
console.log(queue.toString());

queue.deque();
queue.deque();
console.log(queue.toString());
