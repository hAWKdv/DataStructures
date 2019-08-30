import { NumberComparer, CompareSym as CompareSym, CompareFunction } from '../common';

export class Heap<T> extends NumberComparer<T> {
  private _arr: T[] = [];

  constructor(initValue?: T, compare?: CompareFunction<T>) {
    super();
    if (initValue !== undefined) {
      this._arr.push(initValue);
    }
    if (compare) {
      this.compare = compare;
    }
  }

  get empty(): boolean {
    return this._arr.length === 0;
  }

  insert(value: T) {
    this._arr.push(value);
    this._balanceInsert();
  }

  extractMin(): T {
    const first = this._arr[0];

    if (first) {
      if (this._arr.length > 1) {
        const last = this._arr.pop();
        this._arr[0] = last;
        this._balanceExtract(0);
      } else {
        this._arr = [];
      }
    }

    return first;
  }

  private _balanceInsert() {
    let child = this._arr.length - 1;
    let parent = this._getParentIdx(child);

    while (this.compare(this._arr[parent], CompareSym.GT, this._arr[child])) {
      const swap = this._arr[parent];
      this._arr[parent] = this._arr[child];
      this._arr[child] = swap;

      child = parent;
      parent = this._getParentIdx(child);
    }
  }

  private _balanceExtract(idx: number) {
    let smallest = idx;
    let leftChild = this._getLeftIdx(idx);
    let rightChild = this._getRightIdx(idx);

    if (this.compare(this._arr[idx], CompareSym.GT, this._arr[leftChild])) {
      smallest = leftChild;
    }
    if (this.compare(this._arr[idx], CompareSym.GT, this._arr[rightChild])) {
      smallest = rightChild;
    }
    if (smallest !== idx) {
      const swap = this._arr[idx];
      this._arr[idx] = this._arr[smallest];
      this._arr[smallest] = swap;
      this._balanceExtract(smallest);
    }
  }

  private _getParentIdx(i: number) {
    return Math.floor((i - 1) / 2);
  }

  private _getRightIdx(i: number) {
    return 2 * i;
  }

  private _getLeftIdx(i: number) {
    return (2 * i) + 1;
  }
}
