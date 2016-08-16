using UnityEngine;
using System.Collections;
using CP.ProChart;

public class UIShowData : CPanel {

	public LineChart m_lineChart;
	private ChartData2D m_chartData = new ChartData2D ();

	protected override void panelStart ()
	{
		m_lineChart.SetValues (ref m_chartData);
		m_chartData [0, 0] = 50;
		m_chartData [0, 1] = 30;
		m_chartData [0, 2] = 70;
		m_chartData [0, 3] = 10;
		m_chartData [0, 4] = 90;
		m_chartData [0, 5] = 150;

		int line = 2;
		m_chartData [line, 0] = 50-10;
		m_chartData [line, 1] = 30-10;
		m_chartData [line, 2] = 70-10;
		m_chartData [line, 3] = 10-10;
		m_chartData [line, 4] = 90-10;
		m_chartData [line, 5] = 150-10;

	}

	private void OnSelectDelegate(int row , int column){

		Debug.LogError (string.Format ("row={0} column={1}", row, column));
	}
	private void OnOverDelegate(int row , int column){
		Debug.LogError (string.Format ("row={0} column={1}", row, column));
	}
	void OnEnable(){

		m_lineChart.onSelectDelegate += OnSelectDelegate;
		m_lineChart.onOverDelegate += OnOverDelegate;

	}
	void OnDisable(){
		m_lineChart.onSelectDelegate -= OnSelectDelegate;
		m_lineChart.onOverDelegate -= OnOverDelegate;
	}

	void Update(){
		m_chartData [0, 5] -= 0.1f;
	}

}
