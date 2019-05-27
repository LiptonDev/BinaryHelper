# BinaryHelper

## How to find position in binary file? (For example we are search money)

Make BinaryReader instance:
```C#
var br = new BinaryReader(File.OpenRead("player.bin"));
```

Pattern: (1 byte = 1 char)
```C#
new byte[] { 0x6D, 0x06F, 0x6E, 0x65, 0x79 } //text: money
```

Now we may find position for money:
```C#
var offset = br.FindPosition(new byte[] { 0x6D, 0x06F, 0x6E, 0x65, 0x79 }, resetPosition: false);
```

Variable 'offset' are equal 'true' if position found and position in file is not end of file:
```C#
if (offset)
    Console.WriteLine($"Money: {br.ReadInt32()}"); //money in file writed as 4 byte.
```

Test file 'player.bin': https://github.com/LiptonOlolo/BinaryHelper/blob/master/player.bin?raw=true
