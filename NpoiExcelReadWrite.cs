using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;

namespace Npoi_Library
{
	public class NpoiExcelReadWrite
	{
		protected XSSFWorkbook workbook;
		protected XSSFSheet sheet;
		protected FileStream file;
		public NpoiExcelReadWrite(string ExcelDocument)
		{
			try
			{
				file = new FileStream(ExcelDocument, FileMode.Open, FileAccess.ReadWrite);
				workbook = new XSSFWorkbook(file);
				file.Close();
			}
			catch(Exception ex)
			{
				
			}
		}
		public void setSheet(string sheetName)
		{
			sheet = workbook.GetSheet(sheetName) as XSSFSheet;
		}
		public void setSheet(int sheetNumber)
        {
			sheet = workbook.GetSheetAt(sheetNumber) as XSSFSheet;
        }
		public void saveFile(string name)
		{
			file = File.Create(name);
			workbook.Write(file);
			file.Close();
		}
		public void createRowsInstance(int rowCount)
		{
			for (int counter = 0; counter < rowCount + 100; counter++)
			{
				sheet.CreateRow(counter);
			}
		}
		public void createRowsRange(int startRow, int numberOfRows)
		{
			for (int counter = startRow; counter <= numberOfRows; counter++)
			{
				sheet.CreateRow(counter);
			}
		}
		public int getLastRow()
		{
			return sheet.LastRowNum;
		}
		public int WriteArray_To_Excel(int rowAvailableCell, int startingCol, string[,] infoArray)
		{
			if(rowAvailableCell+infoArray.GetUpperBound(0) > sheet.LastRowNum)
			{
				createRowsRange(rowAvailableCell, rowAvailableCell + infoArray.GetUpperBound(0));
			}
			for (int rowCounter = 0; rowCounter <= infoArray.GetUpperBound(0); rowCounter++)
			{
				for (int columnCounter = 0; columnCounter <= infoArray.GetUpperBound(1); columnCounter++)
				{
					try
					{
						sheet.GetRow(rowAvailableCell + rowCounter).CreateCell(columnCounter + startingCol);
						sheet.GetRow(rowAvailableCell + rowCounter).GetCell(columnCounter + startingCol).SetCellValue(infoArray[rowCounter, columnCounter]);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			}
			return startingCol + infoArray.GetUpperBound(1) + 1;
		}
	}
}
