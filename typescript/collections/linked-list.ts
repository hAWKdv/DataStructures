class Item<T> {
  value: T;
  next: Item<T> | undefined;

  constructor(v: T) {
    this.value = v;
  }
}

export class LinkedList<T> {
  private _head: Item<T>;
  private _tail: Item<T>;

  add(value: T): LinkedList<T> {
    const newItem = new Item<T>(value);
    if (this._head) {
      this._tail.next = newItem;
    } else {
      this._head = newItem;
    }
    this._tail = newItem;

    return this;
  }

  delete(value: T) {
    let curr = this._head;
    let prev: Item<T> | undefined;

    while (curr) {
      if (value === curr.value) {
        if (prev) {
          prev.next = curr.next;
        } else {
          this._head = curr.next;
        }
        return;
      }
      prev = curr;
      curr = curr.next;
    }
  }

  print(): string {
    let curr = this._head;
    let str = '';

    while (curr) {
      str += `${curr.value} -> ${(curr.next ? curr.next.value : 'NULL' )}\n`;
      curr = curr.next;
    }

    return str;
  }
}

// Demo:

const list = new LinkedList<number>();

list.add(1).add(2).add(3).add(4);
console.log(list.print());

list.delete(1);
console.log(list.print());

list.delete(3);
console.log(list.print());
