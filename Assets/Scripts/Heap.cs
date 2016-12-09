using UnityEngine;
using System.Collections;
using System;
public class Heap<T> where T: IHeapItem<T> {
    T[] items;
    int currentItemCount;

    public int Count {
        get {
            return currentItemCount;
        }
    }

    public Heap(int maxHeapSize) {
        items = new T[maxHeapSize];
    }

    public void Add(T item) {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    void SortUp(T item) {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true) {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0) {
                Swap(item, parentItem);
            }
            else {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    public T RemoveFirst() {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateIteam(T item) {
        SortUp(item);
    }

    public bool Contains(T item) {
        return Equals(items[item.HeapIndex],item);
    }

    void SortDown(T item) {
        while (true) {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            if(childIndexLeft < currentItemCount) {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount) {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) <0) {
                        swapIndex = childIndexRight;
                    }
                }
                if(item.CompareTo(items[swapIndex])< 0) {
                    Swap(item, items[swapIndex]);
                }
                else {
                    return;
                }
            }
            else {
                return;
            }

        }
    }

    void Swap(T iteamA, T iteamB) {
        items[iteamA.HeapIndex] = iteamB;
        items[iteamB.HeapIndex] = iteamA;
        int temp = iteamA.HeapIndex;
        iteamA.HeapIndex = iteamB.HeapIndex;
        iteamB.HeapIndex = temp;
    }
}

public interface IHeapItem<T> : IComparable<T>{
    int HeapIndex {
        get;
        set;
    }

}
