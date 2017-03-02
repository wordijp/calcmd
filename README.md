# calcmd

Windowsに標準で付属している電卓(calc)をコマンド化します。


# Demo

コマンド実行
![コマンド実行](https://github.com/wordijp/calcmd/demo/calcmd.gif)

Pythonから実行して結果を表示
![Pythonから実行](https://github.com/wordijp/calcmd/demo/calcmd_py.gif)


# 使い方

    > calcmd 1+2+3+4
	> 10
	> 
	> calcmd "1+2" "*" "3+4"
	> 13
    > @rem 電卓では入力した順番に計算される


# 対応した操作

- 四則演算(+, -, *, /)
- 数値入力

**※括弧による優先度には対応していません**


## License

[MIT License](http://opensource.org/licenses/MIT).
