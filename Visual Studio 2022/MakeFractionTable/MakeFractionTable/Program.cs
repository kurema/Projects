using System.Reflection.Metadata.Ecma335;

List<(int, int)>[] values = new List<(int, int)>[1001];

for (int i = 2; i <= 1000; i++)
{
	var factors = Enumerable.Range(2, i - 2).Where(j => i % j == 0).ToArray();

	for (int j = 1; j < i; j++)
	{
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

//数字を多数入力して分母を推定する。
while (true)
{
	var l1 = Console.ReadLine()?.Split(',').Select(d => { if (!double.TryParse(d, out var r)) return 0; return (int)(r * 10); }).Where(a => a >= 1 && a < 1000) ?? Array.Empty<int>();
	var l2 = l1.Select(a => values[a].ToArray()).Aggregate((a, b) => a.Where(d => b.Any(c => c.Item2 == d.Item2)).ToArray()).Select(a => a.Item2).ToArray();

	Console.WriteLine(string.Join(",", l2));
}