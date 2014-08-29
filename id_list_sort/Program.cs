// Xamarin Studio on MAcOSX 10.9
using System;
using System.IO;
using System.Text ;  // for Encoding
using System.Collections.Generic;
using System.Collections;

namespace id_list_sort
{
	class MainClass
	{
		/// <summary>
		/// arg1 = file_2D, arg2 = file_3D
		/// </summary>
		/// <param name="s">The command-line arguments.</param>
		public static void Main(string[] s)
		{
			StringBuilder sb2 = new StringBuilder ();
			StringBuilder sb3 = new StringBuilder ();
			string file2D = String.Empty;
			string file3D = String.Empty;

			for (int N = 1; N < Environment.GetCommandLineArgs ().Length; N++) {
				file2D = Environment.GetCommandLineArgs () [1];
				file3D = Environment.GetCommandLineArgs () [2];
			}

			Console.WriteLine (file2D);
			Console.WriteLine (File.Exists(file2D));
			Console.WriteLine (file3D);
			Console.WriteLine (File.Exists(file3D));

			if (File.Exists(file2D) == true && File.Exists(file3D) == true) {

				StreamReader reader2 = new StreamReader (file2D, Encoding.Default);
				StreamReader reader3 = new StreamReader (file3D, Encoding.Default);
				string r2 = reader2.ReadToEnd ();
				string r3 = reader3.ReadToEnd ();
				List<string> IDs2 = new List<string> ();
				List<string> IDs3 = new List<string> ();
				List<string> inchi2 = new List<string> ();
				List<string> inchi3 = new List<string> ();

				Console.WriteLine ("Fin param set!");

				string[] line2 = r2.Replace("\r", "").Split(new char[] {'\n'}) ;
				string[] line3 = r3.Replace("\r", "").Split(new char[] {'\n'}) ;

				Console.WriteLine (line2.Length + ":" + line3.Length);

				foreach (var r in line2) {
					string[] ID = r.Split (new string[] {".2d.mol\t"}, StringSplitOptions.RemoveEmptyEntries);
					if (ID.Length > 1) {
						//string[] line = ID[1].Replace("\r", "").Split(new char[] {'\n'}) ;
						if (ID.Length > 1) {
							IDs2.Add (ID [0].Trim());
							inchi2.Add (ID [1].Trim());
							Console.WriteLine ("ID:" + ID [0].Trim());
							Console.WriteLine ("inchi:" + ID [1].Trim());
						}
					}
				}

				Console.WriteLine ("IDs2:" + IDs2.Count);
				Console.WriteLine ("inchi2:" + inchi2.Count);


				foreach (var r in line3) {
					string[] ID = r.Split (new string[] {".3d.mol\t"}, StringSplitOptions.RemoveEmptyEntries);
					if (ID.Length > 1) {
						//string[] line = ID[1].Replace("\r", "").Split(new char[] {'\n'}) ;
						if (ID.Length > 1) {
							IDs3.Add (ID [0].Trim());
							inchi3.Add (ID [1].Trim());
							Console.WriteLine ("ID:" + ID [0].Trim());
							Console.WriteLine ("inchi:" + ID [1].Trim());
						}
					}
				}

				Console.WriteLine ("IDs3:" + IDs3.Count);
				Console.WriteLine ("inchi3:" + inchi3.Count);


				List<string> IDall = new List<string> ();


				foreach (var i in IDs2) {
					if (!IDall.Contains (i)) {
						IDall.Add (i);
					}
					foreach (var j in IDs3) {
						if (!IDall.Contains (j)) {
							IDall.Add (j);
						}
					}
				}

				IDall.Sort ();


				foreach (var id in IDall) {

					sb2.Append (id);
					sb3.Append (id);

					bool put2 = false;
					bool put3 = false;
					for (var i = 0; i < IDs2.Count; i++) {
						if (id == IDs2 [i]) {
							sb2.AppendLine ("\t" + inchi2 [i]);
							put2 = true;
							break;
						}
					}
					for (var i = 0; i < IDs3.Count; i++) {
						if (id == IDs3 [i]) {
							sb3.AppendLine ("\t" + inchi3 [i]);
							put3 = true;
							break;
						}
					}
					if (put2 == false) {
						sb2.AppendLine ("");
					}
					if (put3 == false) {
						sb3.AppendLine ("");
					}
				}


				DateTime dt = DateTime.Now;
				string dtString = dt.ToString ("yyyyMMddHHmmss");

				StreamWriter writer2 = new StreamWriter ("ID_full_" + dtString + "_2D.txt",
					false,  // 上書き （ true = 追加 ）
					Encoding.UTF8);
				writer2.Write (sb2.ToString ());
				writer2.Close ();

				StreamWriter writer3 = new StreamWriter ("ID_full_" + dtString + "_3D.txt",
					false,  // 上書き （ true = 追加 ）
					Encoding.UTF8);
				writer3.Write (sb3.ToString ());
				writer3.Close ();


				Console.WriteLine ("finished");

			} else {
				Console.WriteLine ("Please check data");
			}
		}
	}
}
