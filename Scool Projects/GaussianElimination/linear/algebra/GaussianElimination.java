package linear.algebra;

public class GaussianElimination
{
    //fields
    private double matrix[][];
    private int cols;
    private int rows;
    //ctor
    public GaussianElimination(int rows,int cols, double[][] matrix)
    {
        this.rows = rows;
        this.cols = cols;
        this.setMatrix(matrix);
    }
    //getters
    public double[][] getMatrix() { return this.matrix; }
    public int getCols() { return this.cols; }
    public int getRows() { return this.rows; }
    //setters
    public void setMatrix(double[][] matrix)
    {
        if(matrix.length == this.rows && matrix[0].length == this.cols)
        {
            this.matrix = matrix;
        }
        else
        {
            throw new IllegalArgumentException();
        }
    }
    //methods
    public void rowEchelonForm()
    {
        int pivotRow = 0;
        int pivotCol = 0;
        while (pivotRow < this.rows && pivotCol < this.cols) 
        {
            int i_max = argMax(pivotCol, pivotRow);
            if (this.matrix[i_max][pivotCol] == 0) 
            {
                pivotCol++;
            }
            else 
            {
                swapRows(pivotRow, i_max);
                for (int i = pivotRow + 1; i < this.rows; i++)
                {
                    multiplyAndAddRow(i, pivotRow, pivotCol);
                }
                multiplyRow(pivotRow, pivotCol);
                pivotRow++;
                pivotCol++;
            }   
        }
    }
    private int argMax(int col, int row)
    {
        double max = Math.abs(this.matrix[row][col]);
        int ind = row;
        for (int i = row+1; i < this.rows; i++)
        {
            double absVal = Math.abs(this.matrix[i][col]);
            if (absVal > max)
            {
                max = absVal;
                ind = i;
            }
        }
        return ind;
    }
    private void swapRows(int row, int i_max) 
    {
        double[][] tempMatrix = this.matrix;
        double[] tempRow = tempMatrix[row];
        tempMatrix[row] = tempMatrix[i_max];
        tempMatrix[i_max] = tempRow;
        this.setMatrix(tempMatrix);
    }
    private void multiplyAndAddRow(int addRow, int mulRow, int colIndex)
    {
        double[][] tempMatrix = this.matrix;
        double m = tempMatrix[addRow][colIndex] / tempMatrix[mulRow][colIndex];
        matrix[addRow][colIndex] = 0;
        for(int i = colIndex+1; i < this.cols; i++)
        {
            tempMatrix[addRow][i] = tempMatrix[addRow][i] - tempMatrix[mulRow][i] * m;
        }
        this.setMatrix(tempMatrix);
    }
    private void multiplyRow(int rowInd, int colInd)
    {
        double[][] tempMatrix = this.matrix;
        double m = tempMatrix[rowInd][colInd];
        for(int i = colInd; i < this.cols; i++)
        {
            tempMatrix[rowInd][i] /= m;
        }
        this.setMatrix(tempMatrix);
    }
    public void backSubstitution()
    {
        int pivotRow = this.rows-1;
        int pivotCol = this.cols-2;
        while (pivotRow >= 0 && pivotCol >= 0) 
        {
            if (this.matrix[pivotRow][pivotCol] == 0) 
            {
                throw new IllegalArgumentException();
            }
            else 
            {
                multiplyRow(pivotRow, pivotCol);
                for (int i = pivotRow - 1; i >= 0; i--)
                {
                    multiplyAndAddRow(i, pivotRow, pivotCol);
                }
                pivotRow--;
                pivotCol--;
            }   
        }
    }
    public void print()
    {
        char[] helper = {'x','y','z'};
        for(int i = 0; i < this.rows; i++)
        {
            for(int j = 0; j < this.cols; j++)
            {
                if(this.matrix[i][j] >= 0 && j < this.cols-1)
                {
                    System.out.print("+"+this.matrix[i][j]+helper[j]);
                }
                else if(j < this.cols-1)
                {
                    System.out.print(""+this.matrix[i][j]+helper[j]);
                }
                else
                {
                    System.out.print("="+this.matrix[i][j]);
                }
            }
            System.out.println("");
        }
    }

    //XDDD
    private void checkMatrixDimensions(double[][] matrix)
    {
        if(matrix.length != this.rows || matrix[0].length != this.cols)
        {
            throw new IllegalArgumentException();
        }
    }
}