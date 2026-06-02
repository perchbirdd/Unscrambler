using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
using Iced.Intel;

namespace Unscrambler.CodeGenerator;

public static partial class Program {
	private static readonly VersionConstantsStub Result = new();
	private const ulong ImageBase = 0x140000000;
	private static readonly byte[] OpcodeSignature = ConvertHexStringToBytes("?? ?? ?? 2B C8 ?? 8B ?? 8A ?? ?? ?? ?? 41 81");
	private static void D(string s) => $"\t[D] {s}".WithColor(ConsoleColor.DarkGray);
	private static void I(string s) => $"[I] {s}".WithColor(ConsoleColor.Green);
	private static void W(string s) => $"[W] {s}".WithColor(ConsoleColor.Yellow);
	private static void E(string s) => $"[W] {s}".WithColor(ConsoleColor.Red);

	private static void WithColor(this string text, ConsoleColor color) {
		Console.ForegroundColor = color;
		Console.WriteLine(text);
		Console.ResetColor();
	}

	[GeneratedRegex(@"^mov .+?,\[.+\+(?<addr>[0123456789ABCDEF]{7})h\]$")]
	private static partial Regex RegexMovAddr();

	[GeneratedRegex(@"^imul .+?,\[.+\+(?<addr>[0123456789ABCDEF]{7})h\]$")]
	private static partial Regex RegexImulAddr();

	[GeneratedRegex("^imul (.+?,)?.+?,(?<val>[0123456789ABCDEF]+)h$")]
	private static partial Regex RegexImul();

	[GeneratedRegex("^call 0+14(?<addr>[0123456789ABCDEF]{7})h$")]
	private static partial Regex RegexCallAddr();

	[GeneratedRegex("^jn?[z|e] .+? 0+14(?<addr>[0123456789ABCDEF]{7})h$")]
	private static partial Regex RegexDeriveSubCallAddr();

	[GeneratedRegex("^cmp .+?,(?<val>.+?)h$")]
	private static partial Regex RegexCmp();

	[GeneratedRegex(@"^add .+?,\[.+\*4\+(?<addr>[0123456789ABCDEF]{7})h\]$")]
	private static partial Regex RegexAddAddr();

	private class VersionConstantsStub {
		public byte? ObfuscationEnabledMode;
		public string? GameVersion;

		public readonly long[] TableOffsets = new long[3];
		public readonly int[] TableSizes = new int[3];
		public readonly int[] TableRadixes = new int[3];
		public readonly int[] TableMax = new int[3];

		public long? MidTableOffset;
		public int? MidTableSize;
		public long? DayTableOffset;
		public int? DayTableSize;
		public long? OpcodeKeyTableOffset;
		public int? OpcodeKeyTableSize;
		public int? InitZoneOpcode;
		public int? UnknownObfuscationInitOpcode;

		public int? ObfuscatedOpcodesPlayerSpawn;
		public int? ObfuscatedOpcodesNpcSpawn;
		public int? ObfuscatedOpcodesNpcSpawn2;
		public int? ObfuscatedOpcodesActionEffect01;
		public int? ObfuscatedOpcodesActionEffect08;
		public int? ObfuscatedOpcodesActionEffect16;
		public int? ObfuscatedOpcodesActionEffect24;
		public int? ObfuscatedOpcodesActionEffect32;
		public int? ObfuscatedOpcodesStatusEffectList;
		public int? ObfuscatedOpcodesStatusEffectList3;
		public int? ObfuscatedOpcodesExamine;
		public int? ObfuscatedOpcodesUpdateGearset;
		public int? ObfuscatedOpcodesUpdateParty;
		public int? ObfuscatedOpcodesActorControl;
		public int? ObfuscatedOpcodesActorCast;
		public int? ObfuscatedOpcodesUnknownEffect01;
		public int? ObfuscatedOpcodesUnknownEffect16;
		public int? ObfuscatedOpcodesActionEffect02;
		public int? ObfuscatedOpcodesActionEffect04;

		public override string ToString() =>
			$$"""
			  public static class GameConstants {
			  	public static VersionConstants ForNew() => new() {
			  		GameVersion = "{{GameVersion}}",
			  		TableOffsets = [0x{{TableOffsets[0]:X}}, 0x{{TableOffsets[1]:X}}, 0x{{TableOffsets[2]:X}}],
			  		TableSizes = [{{TableSizes[0]}} * 4, {{TableSizes[1]}} * 4, {{TableSizes[2]}} * 4],
			  		TableRadixes = [{{TableRadixes[0]}}, {{TableRadixes[1]}}, {{TableRadixes[2]}}],
			  		TableMax = [{{TableMax[0]}}, {{TableMax[1]}}, {{TableMax[2]}}],	
			  		MidTableOffset = 0x{{MidTableOffset:X}},
			  		MidTableSize = {{MidTableSize}} * 8,
			  		DayTableOffset = 0x{{DayTableOffset:X}},
			  		DayTableSize = {{DayTableSize}} * 4,
			  		OpcodeKeyTableSize = {{OpcodeKeyTableSize}} * 4,
			  		OpcodeKeyTableOffset = 0x{{OpcodeKeyTableOffset:X}},
			  		ObfuscationEnabledMode = {{ObfuscationEnabledMode}},
			  		InitZoneOpcode = 0x{{InitZoneOpcode:X}},
			  		UnknownObfuscationInitOpcode = 0x{{UnknownObfuscationInitOpcode:X}},
			  		ObfuscatedOpcodes = new Dictionary<string, int> {
			  			{ "PlayerSpawn", 0x{{ObfuscatedOpcodesPlayerSpawn:X}} },
			  			{ "NpcSpawn", 0x{{ObfuscatedOpcodesNpcSpawn:X}} },
			  			{ "NpcSpawn2", 0x{{ObfuscatedOpcodesNpcSpawn2:X}} },
			  			{ "ActionEffect01", 0x{{ObfuscatedOpcodesActionEffect01:X}} },
			  			{ "ActionEffect08", 0x{{ObfuscatedOpcodesActionEffect08:X}} },
			  			{ "ActionEffect16", 0x{{ObfuscatedOpcodesActionEffect16:X}} },
			  			{ "ActionEffect24", 0x{{ObfuscatedOpcodesActionEffect24:X}} },
			  			{ "ActionEffect32", 0x{{ObfuscatedOpcodesActionEffect32:X}} },
			  			{ "StatusEffectList", 0x{{ObfuscatedOpcodesStatusEffectList:X}} },
			  			{ "StatusEffectList3", 0x{{ObfuscatedOpcodesStatusEffectList3:X}} },
			  			{ "Examine", 0x{{ObfuscatedOpcodesExamine:X}} },
			  			{ "UpdateGearset", 0x{{ObfuscatedOpcodesUpdateGearset:X}} },
			  			{ "UpdateParty", 0x{{ObfuscatedOpcodesUpdateParty:X}} },
			  			{ "ActorControl", 0x{{ObfuscatedOpcodesActorControl:X}} },
			  			{ "ActorCast", 0x{{ObfuscatedOpcodesActorCast:X}} },
			  			{ "UnknownEffect01", 0x{{ObfuscatedOpcodesUnknownEffect01:X}} },
			  			{ "UnknownEffect16", 0x{{ObfuscatedOpcodesUnknownEffect16:X}} },
			  			{ "ActionEffect02", 0x{{ObfuscatedOpcodesActionEffect02:X}} },
			  			{ "ActionEffect04", 0x{{ObfuscatedOpcodesActionEffect04:X}} }
			  		}
			  	};
			  }
			  // Done. Press Enter to exit.
			  """;
	}

	private static byte[] ConvertHexStringToBytes(string hex) {
		hex = hex.Replace(" ", "").Replace("??", "00");
		var bytes = new byte[hex.Length / 2];
		for (var i = 0; i < bytes.Length; i++)
			bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
		return bytes;
	}

	private class FfxivReverseParser {
		private readonly byte[] _codeData;
		private readonly int _codeRva;

		public FfxivReverseParser(string filePath) {
			var _peData = File.ReadAllBytes(filePath);
			var _peHeaders = new PEHeaders(new MemoryStream(_peData));
			var section = _peHeaders.SectionHeaders.FirstOrDefault(x => x.Name.Equals(".text", StringComparison.OrdinalIgnoreCase));
			var data = new byte[section.SizeOfRawData];
			Buffer.BlockCopy(_peData, section.PointerToRawData, data, 0, data.Length);
			_codeData = data;
			_codeRva = section.VirtualAddress;
		}

		public int LocatePacketDispatcherFunc() {
			for (var i = 0; i < _codeData.Length - OpcodeSignature.Length; i++)
				if (!OpcodeSignature.Where((t, j) => t != 0 && _codeData[i + j] != t).Any())
					return _codeRva + i;
			throw new Exception("未找到签名！");
		}

		public List<Instruction> DisassembleFunc(int rva, int MaxLength = 0x1000) {
			List<Instruction> res = [];
			var decoder = Decoder.Create(64,
				new ByteArrayCodeReader(_codeData, rva - _codeRva, MaxLength),
				ImageBase | (uint)rva);
			D($"Begin: 0x{decoder.IP:X}");
			while (true) {
				var dec = decoder.Decode();
				if (dec.Mnemonic == Mnemonic.INVALID || dec is { Mnemonic: Mnemonic.Int3, Length: 1 }) break;
				res.Add(dec);
			}
			return res;
		}

		public int FindFunctionStartRva(int middleRva) {
			var currentOffset = middleRva - _codeRva;
			while (currentOffset > 0) {
				currentOffset--;
				var code = _codeData[currentOffset];
				// push rbp (0x55) Means Func Start
				if (code == 0x55) return _codeRva + currentOffset;
				// int3 (0xCC) Between Funcs
				if (code == 0xCC && _codeData[currentOffset - 1] == 0xCC) return _codeRva + currentOffset + 1;
			}
			throw new Exception("Can't Find Start of Function.");
		}
	}

	public static void Main(string[] args) {
		var exe = args.Length == 1 ? args[0] : @"C:\Program Files (x86)\上海数龙科技有限公司\最终幻想XIV\game\ffxiv_dx11.exe";
		if (!File.Exists(exe)) {
			E("File not found.");
			return;
		}
		var directory = Directory.GetParent(exe)!.FullName;
		var verFile = Path.Combine(directory, "ffxivgame.ver");
		if (!File.Exists(verFile))
			W("Version file not found.");
		else
			Result.GameVersion = File.ReadAllText(verFile);
		var parser = new FfxivReverseParser(exe);
		var packetFuncRva = parser.FindFunctionStartRva(parser.LocatePacketDispatcherFunc() - 3);
		I($"PacketDispatcher_OnReceivePacket: 0x{packetFuncRva:X}");
		var instructions = parser.DisassembleFunc(packetFuncRva);
		var regexMovAddr = RegexMovAddr();
		var regexImulAddr = RegexImulAddr();
		var regexImul = RegexImul();
		var regexCallAddr = RegexCallAddr();
		var regexDeriveSubCallAddr = RegexDeriveSubCallAddr();
		var regexCmp = RegexCmp();
		var regexAddAddr = RegexAddAddr();
		for (var index = 0; index < instructions.Count; index++) {
			var instr = instructions[index];
			var sil = instr.ToString();
			var regexMovMatch = regexMovAddr.Match(sil);
			if (!regexMovMatch.Success || Result.OpcodeKeyTableOffset != null) continue;
			Result.OpcodeKeyTableOffset = Convert.ToInt64(regexMovMatch.Groups["addr"].Value, 16);
			I($"OpcodeKeyTableOffset Set: 0x{Result.OpcodeKeyTableOffset:X}");
			while (true) {
				index--;
				instr = instructions[index];
				var regexImulMatch = regexImul.Match(instr.ToString());
				if (!regexImulMatch.Success) continue;
				Result.OpcodeKeyTableSize = Convert.ToInt32(regexImulMatch.Groups["val"].Value, 16);
				I($"OpcodeKeyTableSize Set: 0x{Result.OpcodeKeyTableSize:X}");
				break;
			}
			List<int> diriveSubs = [];
			while (true) {
				index--;
				instr = instructions[index];
				var regexCallMatch = regexCallAddr.Match(instr.ToString());
				if (!regexCallMatch.Success || Result.TableOffsets.Any(i => i != 0)) continue;
				var Dirive = Convert.ToInt32(regexCallMatch.Groups["addr"].Value, 16);
				I($"Dirive: 0x{Dirive:X}");
				var DiriveInstructions = parser.DisassembleFunc(Dirive);
				foreach (var il in DiriveInstructions) {
					var regexDiriveSubCallMatch = regexDeriveSubCallAddr.Match(il.ToString());
					if (!regexDiriveSubCallMatch.Success) continue;
					var addr = Convert.ToInt32(regexDiriveSubCallMatch.Groups["addr"].Value, 16);
					I($"Dirive Sub: 0x{addr:X}");
					diriveSubs.Add(addr);
				}
				break;
			}
			if (diriveSubs.Count != 3) {
				W($"Invalid DiriveSubs Length({diriveSubs.Count})");
				diriveSubs = diriveSubs[..3];
			}
			while (true) {
				instr = instructions[--index];
				var regecCmpMatch = regexCmp.Match(instr.ToString());
				if (!regecCmpMatch.Success) continue;
				if (Result.ObfuscationEnabledMode == null) {
					var val = Convert.ToByte(regecCmpMatch.Groups["val"].Value, 16);
					Result.ObfuscationEnabledMode = val;
					I($"ObfuscationEnabledMode Set: (byte){val}");
					continue;
				}
				if (Result.UnknownObfuscationInitOpcode == null) {
					var val = Convert.ToInt32(regecCmpMatch.Groups["val"].Value, 16);
					Result.UnknownObfuscationInitOpcode = val;
					I($"UnknownObfuscationInitOpcode Set: 0x{val:X}");
					break;
				}
			}
			var diriveSubInstructions = new List<Instruction>[3];
			for (var i = 0; i < 3; i++) {
				var dsub = diriveSubs[i];
				I($"Dirive Sub {i}: 0x{dsub:X}");
				var DiriveSubInstructions = parser.DisassembleFunc(dsub);
				diriveSubInstructions[i] = DiriveSubInstructions;
				for (var index1 = 0; index1 < DiriveSubInstructions.Count; index1++) {
					var il = DiriveSubInstructions[index1];
					var imulMatch = regexImulAddr.Match(il.ToString());
					if (!imulMatch.Success) continue;
					var val = Convert.ToInt32(imulMatch.Groups["addr"].Value, 16);
					int size;
					var indexBak = index1;
					while (true) {
						var il1 = DiriveSubInstructions[--index1];
						var imulMatch2 = regexImul.Match(il1.ToString());
						if (!imulMatch2.Success) continue;
						size = Convert.ToInt32(imulMatch2.Groups["val"].Value, 16);
						break;
					}
					index1 = indexBak;
					D($"DireveSub{i} Imul: 0x{val:X} ({il})");
					if (Result.MidTableOffset == null) {
						Result.MidTableOffset = val;
						Result.MidTableSize = size;
						I($"MidTableOffset Set: 0x{val:X}");
						I($"MidTableSize Set: {size}");
					} else if (Result.MidTableOffset != val)
						throw new Exception($"MidTableOffset Mismatch ({Result.MidTableOffset} != {val})");
					else if (Result.MidTableSize != size)
						throw new Exception($"MidTableSize Mismatch ({Result.MidTableSize} != {size})");
				}
			}
			HashSet<int>[] movs = [[], [], []];
			Dictionary<int, int> movsCount = [];
			for (var i = 0; i < diriveSubInstructions.Length; i++) {
				var DiriveSubInstructions = diriveSubInstructions[i];
				for (var index1 = 0; index1 < DiriveSubInstructions.Count; index1++) {
					var il = DiriveSubInstructions[index1];
					var movMatch = regexMovAddr.Match(il.ToString());
					if (movMatch.Success) {
						var val = Convert.ToInt32(movMatch.Groups["addr"].Value, 16);
						movsCount.TryAdd(val, 0);
						movsCount[val]++;
						D($"DireveSub{i} Mov: 0x{val:X} ({il})");
						movs[i].Add(val);
					}
					var addMatch = regexAddAddr.Match(il.ToString());
					if (addMatch.Success) {
						var val = Convert.ToInt32(addMatch.Groups["addr"].Value, 16);
						int size;
						var indexBak = index1;
						while (true) {
							var il1 = DiriveSubInstructions[--index1];
							var imulMatch = regexImul.Match(il1.ToString());
							if (!imulMatch.Success) continue;
							size = Convert.ToInt32(imulMatch.Groups["val"].Value, 16);
							break;
						}
						index1 = indexBak;
						if (Result.DayTableOffset == null) {
							Result.DayTableOffset = val;
							Result.DayTableSize = size;
							I($"DayTableOffset Set: 0x{val:X}");
							I($"DayTableSize Set: {size}");
						} else if (Result.DayTableOffset != val)
							throw new Exception($"DayTableOffset Mismatch ({Result.DayTableOffset} != {val})");
						else if (Result.DayTableSize != size)
							throw new Exception($"DayTableSize Mismatch ({Result.DayTableSize} != {size})");
					}
				}
			}
			if (movs[0].Count != 1) throw new Exception("Invalid Dirive Sub0 Length.");
			if (movs[1].Count != 2) throw new Exception("Invalid Dirive Sub1 Length.");
			if (movs[2].Count != 3) throw new Exception("Invalid Dirive Sub2 Length.");
			var tmp = movs[0].Intersect(movs[1]).Intersect(movs[2]).ToList();
			if (tmp.Count != 1) throw new Exception("Invalid Intersect(0, 1, 2).");
			var tval = tmp[0];
			int[] tlval = [tval];
			Result.TableOffsets[0] = tval;
			movs[1].ExceptWith(tlval);
			movs[2].ExceptWith(tlval);
			tmp = movs[1].Intersect(movs[2]).ToList();
			if (tmp.Count != 1) throw new Exception("Invalid Intersect(1, 2).");
			tval = tmp[0];
			tlval = [tval];
			Result.TableOffsets[1] = tval;
			movs[2].ExceptWith(tlval);
			tmp = movs[2].ToList();
			if (tmp.Count != 1) throw new Exception("Invalid Intersect(2).");
			tval = tmp[0];
			Result.TableOffsets[2] = tval;
			I($"TableOffsets Set: [0x{Result.TableOffsets[0]:X}, 0x{Result.TableOffsets[1]:X}, 0x{Result.TableOffsets[2]:X}]");
			for (var x = 0; x < 3; x++) {
				for (var i = diriveSubInstructions[x].Count - 1; i >= 0; i--) {
					var il = diriveSubInstructions[x][i];
					var movMatch = regexMovAddr.Match(il.ToString());
					if (!movMatch.Success) continue;
					var val = Convert.ToInt32(movMatch.Groups["addr"].Value, 16);
					if (Result.TableOffsets[x] != val) continue;
					while (i > 0) {
						il = diriveSubInstructions[x][--i];
						var matchregexImul = regexImul.Match(il.ToString());
						if (!matchregexImul.Success) continue;
						var valm = Convert.ToInt32(matchregexImul.Groups["val"].Value, 16);
						D($"({x}) {il}");
						if (Result.TableRadixes[x] == 0) {
							Result.TableRadixes[x] = valm;
							continue;
						}
						if (Result.TableMax[x] == 0) {
							Result.TableMax[x] = valm;
							break;
						}
					}
				}
			}
			I($"TableRadixes Set: [{Result.TableRadixes[0]}, {Result.TableRadixes[1]}, {Result.TableRadixes[2]}]");
			I($"TableMax Set: [{Result.TableMax[0]}, {Result.TableMax[1]}, {Result.TableMax[2]}]");
			Result.TableSizes[0] = Result.TableRadixes[0] * Result.TableMax[0];
			Result.TableSizes[1] = Result.TableRadixes[1] * Result.TableMax[1];
			Result.TableSizes[2] = Result.TableRadixes[2] * Result.TableMax[2];
			break;
		}
		if (Result.GameVersion != null) {
			try {
				I("Waiting 4 Remote Opcodes...");
				using var httpClient = new HttpClient();
				var shortVersion = "CN_" + Result.GameVersion[..^".0000.0000".Length]; //CN Global KR
				var result = httpClient.GetStringAsync($"https://raw.githubusercontent.com/Yarukon/FFXIVNetworkOpcodes/master/output/{shortVersion}/machina.txt").Result;
				foreach (var line in result.Split()) {
					var sp = line.Split("|");
					var val = Convert.ToInt32(sp[1], 16);
					switch (sp[0]) {
						case "PlayerSpawn": Result.ObfuscatedOpcodesPlayerSpawn = val; break;
						case "NpcSpawn": Result.ObfuscatedOpcodesNpcSpawn = val; break;
						case "NpcSpawn2": Result.ObfuscatedOpcodesNpcSpawn2 = val; break;
						case "Ability1": Result.ObfuscatedOpcodesActionEffect01 = val; break;
						case "Ability8": Result.ObfuscatedOpcodesActionEffect08 = val; break;
						case "Ability16": Result.ObfuscatedOpcodesActionEffect16 = val; break;
						case "Ability24": Result.ObfuscatedOpcodesActionEffect24 = val; break;
						case "Ability32": Result.ObfuscatedOpcodesActionEffect32 = val; break;
						case "StatusEffectList": Result.ObfuscatedOpcodesStatusEffectList = val; break;
						case "StatusEffectList3": Result.ObfuscatedOpcodesStatusEffectList3 = val; break;
						case "ActorControl": Result.ObfuscatedOpcodesActorControl = val; break;
						case "ActorCast": Result.ObfuscatedOpcodesActorCast = val; break;
					}
				}
				var result2 = httpClient.GetStringAsync($"https://raw.githubusercontent.com/Yarukon/FFXIVNetworkOpcodes/master/output/{shortVersion}/opcodes.json").Result;
				using var doc = JsonDocument.Parse(result2);
				var list = doc.RootElement.GetProperty("lists");
				foreach (var item in list.GetProperty("ServerZoneIpcType").EnumerateArray().Concat(list.GetProperty("ClientZoneIpcType").EnumerateArray())) {
					var val = item.GetProperty("opcode").GetInt32();
					switch (item.GetProperty("name").GetString()) {
						case "Examine": Result.ObfuscatedOpcodesExamine = val; break;
						case "ModelEquip": Result.ObfuscatedOpcodesUpdateGearset = val; break;
						case "UpdateParty": Result.ObfuscatedOpcodesUpdateParty = val; break;
						case "EventAction8": Result.ObfuscatedOpcodesUnknownEffect01 = val; break;
						case "EventAction16": Result.ObfuscatedOpcodesUnknownEffect16 = val; break;
						case "EventFinish64": Result.ObfuscatedOpcodesActionEffect02 = val; break;
						case "EventFinish128": Result.ObfuscatedOpcodesActionEffect04 = val; break;
						case "InitZone": Result.InitZoneOpcode = val; break;
					}
				}
			} catch (Exception e) {
				E(e.ToString());
			}
		}
		Console.WriteLine(Result);
		Console.ReadLine();
	}
}