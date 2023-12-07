using Microsoft.VisualBasic;

class Program {
    static void Main() {
        long x = 82;
        long maxHoldTime = x;
        long minDistanceToWin = 1522;
        long Awns = 0;
        long wins = 0;

        for (long i = 0; i < maxHoldTime; i++) {

            Awns = x * i;
            x--;
            
            if (Awns > minDistanceToWin) {
                wins++;
            }

            
             
        }
        Console.WriteLine("You've won : {0}", wins);
    }
}

        
//27 25 37 18