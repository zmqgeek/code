    namespace Views
    {
    	public class RecordView 
    	{
    		private RecordDataFieldList<string, string> _fieldUnit
    		
            public RecordView()
            {
    			_fieldUnit.List = new IdValueList<string, string>
    			{            
    				new ListItem<string>((int)RecordVM.Enum.Id.Minutes, RecordVM.Enum.Id.Minutes.Name()),
    				new ListItem<string>((int)RecordVM.Enum.Id.Hours, RecordVM.Enum.Id.Hours.Name())
    			};
            }
    		private void Update()
    		{    
    			RecordVM.Enum eId = DetermineUnit();
    			
                _fieldUnit.Input.Text = _fieldUnit.List.SetSelected((int)eId).Value;
    		}
    	}
    }
