// With Hoare partition scheme (more efficient compared to Lomuto)
function partition<T>(arr: T[], lo: number, hi: number): number {
  const pIdx = Math.floor(lo + (hi - lo) / 2);
  const pivot = arr[pIdx];

  while (true) {
    while (arr[lo] < pivot) {
      lo += 1;
    }
    while (pivot < arr[hi]) {
      hi -= 1;
    }
    if (lo >= hi) {
      return hi;
    }

    const swap = arr[lo];
    arr[lo] = arr[hi];
    arr[hi] = swap;

    lo += 1;
    hi -= 1;
  }
}

export function quickSort<T>(arr: T[], lo: number = 0, hi: number = arr.length - 1) {
  if (lo < hi) {
    const p = partition(arr, lo, hi);
    quickSort(arr, lo, p);
    quickSort(arr, p + 1, hi);
  }
}

// Demo:

const arr = [3, 7, 6, 1, 2, 9, 1];
quickSort(arr);

console.log(arr);
