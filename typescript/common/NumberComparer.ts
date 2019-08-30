import { Comparer, CompareSym } from './interfaces';

export abstract class NumberComparer<T> implements Comparer<T> {
  compare(a: T, sym: CompareSym, b: T) {
    switch (sym) {
      case CompareSym.EQ:
        return a === b;
      case CompareSym.NEQ:
        return a !== b;
      case CompareSym.GT:
        return a > b;
      case CompareSym.LT:
        return a < b;
      case CompareSym.GTE:
        return a >= b;
      case CompareSym.LTE:
        return a <= b;
    }
  }
}
