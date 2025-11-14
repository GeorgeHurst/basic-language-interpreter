# Basic Script Interpreter (BSI)

## 1. Introduction
The **Basic Script Interpreter (BSI)** is a learning project developed to explore the fundamentals of **C#** and language interpretation.  
It began with a simple scope and is designed to expand over time as new features are introduced.

---

## 2. Features
BSI allows execution of commands either **line by line** through the console or via a **script file**.  

**Current capabilities include:**
- Reading and validating scripts  
- Executing supported commands sequentially  
- Managing user-defined variables  

---

## 3. Releases

### Version 1.0

#### Supported Commands

##### `START`
Indicates the **beginning** of a script block.  
All valid BSI scripts must begin with this keyword.  

```bsi
START
```

---

##### `END`
Indicates the **end** of a script block.  
Marks where the interpreter should stop reading the script.

```bsi
END
```

---

##### `SET <var> <value>`
Stores a **variable** with the specified value.  
The variable can be referenced later within the same script.

```bsi
SET name George
SET age 19
```

---

##### `PRINT <value>`
Outputs the specified value to the console.

```bsi
PRINT Hello
```

---

##### `ADD <var1> <var2> <resultvar>`
Adds the two given values and stores the result in the third argument.

```bsi
ADD 5 5 x 
```
---

##### `SUBTRACT <var1> <var2> <resultvar>`
Subtracts the second argument from the first and stores the result in the third argument.

```bsi
SUBTRACT 10 5 x 
```

---

##### `SLEEP <duration>`
Sleeps the program for the specifed number of **seconds**.

```bsi
SLEEP 5
```
<br>


## 4. Example Script

```bsi
START
SET name George
SET age 25
PRINT name
SUBTRACT age 5 old
PRINT age
END
```

---
<BR>

## 5. Future Plans
Planned features include:
- Conditional statements (`IF`, `ELSE`)
- Loops (`WHILE`, `FOR`)
- Error handling and debugging tools
- Better print logic (`" "`)
- Create new script

---
<br>

## 6. Author
**Created by:** George Hurst  
**Language:** C#  
**Purpose:** Educational / Learning Project  
