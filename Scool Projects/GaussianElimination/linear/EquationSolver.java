package linear;

import linear.algebra.GaussianElimination;

class EquationSolver
{
    public static void main(String[] args)
    {
        double[] nums = stringsToDoubles(args);
        double[][] matrix = new double[args.length][4];
        int indnums = -1;
        for(int i = 0; i < args.length; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                indnums++;
                matrix[i][j] = nums[indnums];
            }
        }
        GaussianElimination ge = new GaussianElimination(matrix.length, matrix[0].length, matrix);
        ge.print();
        System.out.println();
        ge.rowEchelonForm();
        ge.print();
        System.out.println();
        ge.backSubstitution();
        ge.print();
    }

    public static double[] stringsToDoubles(String[] args)
    {
        double[] nums = new double[args.length*4];
        int numsind = -1;
        for(int i = 0; i < args.length; i++)
        {
            String[] elements = args[i].split(",");
            for(int j = 0; j < elements.length; j++)
            {
                numsind++;
                nums[numsind] = Double.parseDouble(elements[j]);
            }
        }
        return nums;
    }
}