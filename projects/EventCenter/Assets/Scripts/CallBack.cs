//delegate 委託
//這裡可以理解為撥號的類型轉接
public delegate void CallBack();

//T是一個泛型，可以是任意類型，例如int，string等等
//X Y Z 同樣是泛型，可以是任意類型，例如int，string等等
public delegate void CallBack<T>(T arg1);
public delegate void CallBack<T, X>(T arg1, X arg2);
public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);