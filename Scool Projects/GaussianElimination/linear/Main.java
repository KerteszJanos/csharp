package linear;

import linear.algebra.GaussianElimination;

class Main
{
    public static void main(String[] args)
    {
	double[][] matrix2 = new double[][] {
		{ 1.0, 3.0,  1.0,  9.0 },
		{ 1.0, 1.0, -1.0,  1.0 },
		{ 3.0, 11.0, 5.0, 35.0 } };

        GaussianElimination ge = new GaussianElimination(3, 4, matrix2);
		ge.print();
        System.out.println("-----------------------------------------------------------------------------");
        ge.rowEchelonForm();
        ge.print();
        System.out.println("-----------------------------------------------------------------------------");
        ge.backSubstitution();
        ge.print();
    }
}