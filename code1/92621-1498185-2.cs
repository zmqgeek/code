    object[,] array2DBase1 = (object[,]) MySheet.UsedRange.get_Value(Type.Missing);
    
    object[,] array2DBase1 = array2DBase1.CloneBase0();
    
    for (int row = 0; row < array2DBase1.GetLength(0); row++) 
    {
        for (int column = 0; column < array2DBase1.GetLength(1); column++) 
        {
            // Your code goes here...
        }
    }
