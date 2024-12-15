## README.md

# CAAssembler: Assembly Instruction Encoder

### Overview
The `CAAssembler` is a C# application designed to convert assembly-like instructions into a binary-encoded format. developed for INHA_202402_ICE4004_001 CPU.

---

### Supported Instructions

Refer to `OPCODE.cs` for a full list of supported opcodes.

---

### Usage
1. **Prepare Input File**:
   - Create an `input.txt` file with assembly instructions, one per line.
   
     Example:
     ```
     ADD, R1, R2, R3
     BN, 32
     GSTORE, R2
     ```

2. **Run the Program**:
   - Compile and run the project.
   - The program will process the instructions and write the results to `output.txt`.

3. **Check Output**:
   - Successfully parsed instructions are written in binary.
   - Failed instructions are marked as `00000000000000000000000000000000 Failed`.

---

### Example
**Input (`input.txt`):**
```
ADD, 1, 2, 3
SUB, 4, 5, 6
INVALID, 0, 1, 2
```

**Output (`output.txt`):**
```
000000000010001000110
000001000100010001110
00000000000000000000000000000000 Failed
```

---

### Dependencies
- **.NET Framework**: Ensure you have .NET installed to compile and run the application.

---

Enjoy using `CAAssembler` to encode your assembly instructions! 🦊