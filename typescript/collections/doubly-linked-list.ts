class Item<T> {
  value: T;
  prev: Item<T> | undefined;
  next: Item<T> | undefined;

  constructor(v: T) {
    this.value = v;
  }
}

export class DoublyLinkedList<T> {
  private _head: Item<T>;
  private _tail: Item<T>;

  add(value: T): DoublyLinkedList<T> {
    const newItem = new Item<T>(value);

    if (this._head) {
      this._tail.next = newItem;
    } else {
      this._head = newItem;
    }

    const currTail = this._tail;
    this._tail = newItem;
    this._tail.prev = currTail;

    return this;
  }

  delete(value: T): void {
    let curr = this._head;

    while (curr) {
      if (value === curr.value) {
        if (curr.prev) {
          curr.prev.next = curr.next;
          curr.next.prev = curr.prev;
        } else {
          this._head = curr.next;
          this._head.prev = undefined;
        }
        return;
      }
      curr = curr.next;
    }
  }

  toString(): string {
    let c = this._head;
    let str = '';

    while (c) {
      str += `${c.prev ? c.prev.value : 'N'} <- ${c.value} -> ${c.next ? c.next.value : 'N'}\n`;
      c = c.next;
    }

    return str;
  }
}

// Demo:

const list = new DoublyLinkedList<number>();

list.add(1).add(2).add(3).add(4);
console.log(list.toString());

list.delete(1);
console.log(list.toString());

list.delete(3);
console.log(list.toString());

