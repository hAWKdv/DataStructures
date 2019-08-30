export enum CompareSym {
  EQ,
  NEQ,
  GT,
  LT,
  GTE,
  LTE
}

export type CompareFunction<T> = (a: T, sym: CompareSym, b: T) => boolean;

export interface Comparer<T> {
  compare: CompareFunction<T>;
}
