using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Compiler; 

public static class Utils {
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
        foreach (var item in items) action(item);
    }

    public static void ForEachTry<T>(this IEnumerable<T> items,
                                     Action<T> action,
                                     Action<T, Exception> handleExceptions = null,
                                     Action<T> handleFinally = null) {
        var currentItem = default(T);
        try {
            items.ForEach(i => {
                currentItem = i;
                try {
                    action(i);
                } catch (Exception e) {
                    handleExceptions?.Invoke(i, e);
                } finally {
                    handleFinally?.Invoke(i);
                }
            });
        } catch (Exception e) {
            handleExceptions?.Invoke(currentItem, e);
        }
    }
    
    
}