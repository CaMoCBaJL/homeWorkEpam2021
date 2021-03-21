using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BestStringEver
{
    class MyString : IEnumerable
    {
        char[] symbols;
        int arrLength = -1;

        public MyString(string str)
        {
            arrLength = str.Length;
            symbols = new char[arrLength];
            for (int i = 0; i < arrLength; i++)
            {
                symbols[i] = str[i];
            }
        }

        public int Length
        {
            get
            {
                return arrLength;
            } 
        }
    
        public MyString()
        {
            symbols = new char[1];
            arrLength = 0;
        }

        public MyString(char[] charArr)
        {
            symbols = charArr;
        }

        public MyString(StringBuilder str)
        {
            arrLength = str.Length;
            symbols = new char[arrLength];
            for (int i = 0; i < arrLength; i++)
            {
                symbols[i] = str[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MyCharEnumerator GetEnumerator()
        {
            return new MyCharEnumerator(symbols);
        }

        //--------------------------------
        //Вложенный класс MyCharEnumerator
        public class MyCharEnumerator : IEnumerator
        {
            private char[] chars;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public MyCharEnumerator(char[] list)
            {
                chars = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < chars.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public char Current
            {
                get
                {
                    try
                    {
                        return chars[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
        //Конец вложенного класса
        //-----------------------------------
        public char First
        {
            get
            {
                if (arrLength > 0)
                    return symbols[0];
                else throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (arrLength > 0)
                    symbols[0] = value;
                else throw new ArgumentOutOfRangeException();
            }
        }

        public char Last
        {
            get
            {
                if (arrLength > 0)
                    return symbols[arrLength - 1];
                else throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (arrLength > 0)
                    symbols[arrLength - 1] = value;
                else throw new ArgumentOutOfRangeException();
            }
        }

        public MyString Append(char newElem)
        {
            char[] buff = new char[symbols.Length + 1];
            for (int i = 0; i < arrLength; i++)
            {
                buff[i] = symbols[i];
            }
            arrLength++;
            buff[arrLength - 1] = newElem;
            symbols = buff;
            return new MyString(buff);
        }

        public MyString Append(string str)
        {
            char[] buff = new char[symbols.Length + str.Length];
            for (int i = 0; i < arrLength; i++)
            {
                buff[i] = symbols[i];
            }
            for (int i = arrLength; i < arrLength+str.Length; i++)
            {
                buff[i] = str[i - arrLength];
            }
            arrLength += str.Length;
            symbols = buff;
            return new MyString(buff);
        }

        public MyString Append(StringBuilder str)
        {
            char[] buff = new char[symbols.Length + str.Length];
            for (int i = 0; i < arrLength; i++)
            {
                buff[i] = symbols[i];
            }
            for (int i = arrLength; i < arrLength + str.Length; i++)
            {
                buff[i] = str[i - arrLength];
            }
            arrLength += str.Length;
            symbols = buff;
            return new MyString(buff);
        }

        public MyString Append(MyString str)
        {
            char[] buff = new char[symbols.Length + str.Length];
            for (int i = 0; i < arrLength; i++)
            {
                buff[i] = symbols[i];
            }
            for (int i = arrLength; i < arrLength + str.Length; i++)
            {
                buff[i] = str[i - arrLength];
            }
            arrLength += str.Length;
            symbols = buff;
            return new MyString(buff);
        }

        public char[] ToCharArray()
        {
            return symbols;
        }
        public override string ToString()
        {
            return new string(symbols);
        }

        public char this[int index]
        {
            get
            {
                if (index >= 0)
                    return symbols[index];
                else
                    return symbols[arrLength +index];
            }
            set
            {
                if (index >= 0)
                    symbols[index] = value;
                else
                    symbols[arrLength + index] = value;
            }
        }

        public static bool operator !=(MyString s1, MyString s2)
        {
            if (s1.Length == s2.Length)
            {
                for (int i = 0; i < s1.Length; i++)
                    if (s1[i] != s2[i])
                        return true;
                return false;
            }
            else
                return true;
        }

        public static MyString operator + (MyString s1, MyString s2)
        {
            MyString result = new MyString();
            foreach (var item in s1)
                result.Append(item);
            foreach (var item in s2)
                result.Append(item);
            return result;
        }

        public static MyString operator * (MyString s1, int counter)
        {
            for (int i = 0; i < counter; i++)
                s1.Append(s1);
            return s1;
        }

        public MyString Insert(int position, StringBuilder s)
        {
            MyString result = new MyString();
            result = this.Slice(0, position).Append(s) + this.Slice(position);
            arrLength = result.Length;
            symbols = result.ToCharArray();
            return result;
        }

        public MyString Insert(int position, string s)
        {
            MyString result = new MyString();
            result = this.Slice(0, position).Append(s) + this.Slice(position);
            arrLength = result.Length;
            symbols = result.ToCharArray();
            return result;
        }

        public MyString Insert(int position, char c)
        {
            MyString result = new MyString();
            result = this.Slice(0, position).Append(c) + this.Slice(position);
            arrLength = result.Length;
            symbols = result.ToCharArray();
            return result;
        }

        public bool Contains(char c)
        {
            foreach (var item in symbols)
            {
                if (item == c)
                    return true;
            }
            return false;
        }

        public bool Contains(string s)
        {
            return this.Find(s) != -1;
        }

        public static bool IsEmpty(MyString s)
        {
            return s.arrLength == 0;
        }

        public static bool IsWhiteSpace(MyString s)
        {
            for (int index = 0; index < s.Length; ++index)
            {
                if (!char.IsWhiteSpace(s[index]))
                    return false;
            }
            return true;
        }

        public static bool operator == (MyString s1, MyString s2)
        {
            if (s1.Length == s2.Length)
            {
                for (int i = 0; i < s1.Length; i++)
                    if (s1[i] != s2[i])
                        return false;
                return true;
            }
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (this == (MyString)obj)
                return true;
            return false;
        }

        public int Find(string s)
        {
            bool wrongIteration = false;
            for (int i = 0; i < arrLength; i++)
            {
                if (symbols[i] == s[0] && i + s.Length-1 < arrLength)
                {
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (s[j] != symbols[i + j])
                        {
                            wrongIteration = true;
                            break;
                        }
                    }
                    if (wrongIteration)
                        continue;
                    else return i;
                }
            }
            return -1;
        }

        public MyString ToLower()
        {
            MyString result = new MyString();
            for (int i = 0; i < arrLength; i++)
            {
                symbols[i] = char.ToLower(symbols[i]);
                result.Append(symbols[i]);
            }
            return result;
        }

        public MyString ToHigher()
        {
            MyString result = new MyString();
            for (int i = 0; i < arrLength; i++)
            {
                symbols[i] = char.ToUpper(symbols[i]);
                result.Append(symbols[i]);
            }
            return result;
        }

        public int Find(char c)
        {
            for (int i = 0; i < arrLength; i++)
            {
                if (symbols[i] == c)
                    return i;
            }
            return -1;
        }

        public MyString Remove(char value)
        {
            MyString result = new MyString();
            foreach (char item in symbols)
            {
                if (item != value)
                    result.Append(item);
            }
            return result;
        }

        public MyString Remove(int startIndex, int endIndex)
        {
            MyString result = new MyString();
            int i;
            int j;
            if (startIndex >= 0)
                if (endIndex >= 0)
                {
                    i = startIndex;
                    j = endIndex;
                }
                else
                {
                    i = startIndex;
                    j = arrLength + endIndex;
                }
            else if (endIndex >= 0)
            {
                i = arrLength + startIndex;
                j = endIndex;
            }
            else
            {
                i = arrLength + startIndex;
                j = arrLength + endIndex;
            }
            if (i >= j)
                for (int k = 0; k < arrLength; k++)
                {
                    if (k < i && k > j)
                        result.Append(symbols[k]);
                }
            else if(i < j)
            {
                for (int k = 0; k < arrLength; k++)
                {
                    if (k > i && k < j)
                        result.Append(symbols[k]);
                }
            }
            return result;
        }

        public MyString RemoveAt(int position)
        {
            if (position == 0)
                return Slice(1, -1, 1);
            if (position < 0)
                position = arrLength + position;
                char[] buff = new char[arrLength - 1];
                for (int i = 0; i < position; i++)
                    buff[i] = symbols[i];
                for (int i = position + 1; i < arrLength; i++)
                    buff[i] = symbols[i];
                return new MyString(buff);
        }

        public MyString Slice(int startIndex = 0, int endIndex = -1, uint step = 1)
        {
            if (startIndex == 0 && endIndex == -1 && step == 1)
                return new MyString(symbols);
            else
            {
                int i;
                int j;
                if(startIndex >= 0)
                    if(endIndex >= 0)
                    {
                        i = startIndex;
                        j = endIndex;
                    }
                else
                    {
                        i = startIndex;
                        j = arrLength + endIndex;
                    }
                else
                    if(endIndex >= 0)
                {
                    i = arrLength + startIndex;
                    j = endIndex;
                }
                else
                {
                    i = arrLength + startIndex;
                    j = arrLength + endIndex;
                }

                MyString result = new MyString();
                if (i < j)
                {
                    while (i <= j)
                    {
                        result.Append(symbols[i]);
                        i += (int)(step % arrLength);
                    }
                }
                else
                {
                    while (i >= j)
                    {
                        result.Append(symbols[i]);
                        i -= (int)(step % arrLength);
                    }
                }
                return result;
            }
        }

    }
}
