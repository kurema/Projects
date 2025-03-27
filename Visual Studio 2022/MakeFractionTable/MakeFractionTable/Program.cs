using System.Reflection.Metadata.Ecma335;

List<(int, int)>[] values = new List<(int, int)>[1001];

for (int i = 2; i <= 1000; i++)
{
	var factors = Enumerable.Range(2, i - 2).Where(j => i % j == 0).ToArray();

	for (int j = 1; j < i; j++)
	{
		//下行は必要に応じてコメントアウト
		if (factors.Any(f => j % f == 0)) continue;
		var cnt = (int)double.Round(j * 1000.0 / i);
		//var cnt = (int)double.Floor(j * 1000.0 / i);
		var l = values[cnt];
		l ??= new List<(int, int)>();
		l.Add((j, i));
		values[cnt] = l;
	}
}


//CSV形式で出力する
/*
for (int i = 0; i < 1001; i++)
{
	if (values[i] is null) continue;

	Console.Write($"{i / 10.0:0.0},");
	//Console.WriteLine(values[i][0].Item2);
	int cnt = 0;
	foreach (var (n, d) in values[i])
	{
		Console.Write($"\"{n}/{d}\",");
		cnt++;
		if (cnt > 10) break;
	}
	Console.WriteLine();
}
*/


var data = new string[] {
	"7.7,23.1,15.4",
	"9.4,6.4,6.1,7.8,11.1,12.7,20.9,16.7,7.1,1.9",
	"7.7,2.7,7.3,9.6,13.2,15.3,21.2,15.3,6.2,1.6",
	"5.6,2.4,4.0,12.0,16.8,16.4,23.2,13.6,4.8,1.2",
	"4.1,1.0,3.6,5.2,18.7,19.7,26.4,14.5,5.2,1.6",
	"2.3,1.6,2.3,3.9,9.3,23.3,31.8,17.1,6.2,2.3",
	"3.4,1.7,4.3,6.0,10.3,35.3,25.0,8.6,3.4",
	"4.4,2.2,11.1,42.2,17.8,6.7",
	"7.7,23.1,30.8",
	"16.7,50.0"
};

foreach (var text in data)
{
	var l1 = text.Split(',').Select(d => { if (!double.TryParse(d, out var r)) return 0; return (int)(r * 10); }).Where(a => a >= 1 && a < 1000).ToArray() ?? Array.Empty<int>();
	if (l1.Length == 0) continue;
	var l2 = l1.Select(a => values[a].ToArray()).Aggregate((a, b) => a.Where(d => b.Any(c => d.Item2 == c.Item2)).ToArray()).Select(a => a.Item2).ToArray();

	Console.WriteLine(l2.First());
	//Console.WriteLine(string.Join(",", l2));
}


//数字を多数入力して分母を推定する。
while (true)
{
	var l1 = Console.ReadLine()?.Split(',').Select(d => { if (!double.TryParse(d, out var r)) return 0; return (int)(r * 10); }).Where(a => a >= 1 && a < 1000).ToArray() ?? Array.Empty<int>();
	if (l1.Length == 0) continue;
	//公倍数とか考えないといけない。下だと怪しい。要確認。
	//if (factors.Any(f => j % f == 0)) continue;をコメントアウトすれば上の方のコードと同じで問題ないはず。
	var l2 = l1.Select(a => values[a].ToArray()).Aggregate((a, b) => a.Where(d => b.Any(c => d.Item2 % c.Item2 == 0)).ToArray()).Select(a => a.Item2).ToArray();

	Console.WriteLine(string.Join(",", l2));
}