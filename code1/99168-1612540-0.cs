    class C {
        private object mylock = new object();
        
        public int A {
        
          get {
            int result;
            lock(mylock) {
            result = mA; 
            }
            return result;
          } 
        
          set { 
             lock(mylock) { 
                mA = value; 
             }
          }
        }
    }
    C obj = new C;
    C.A++;
