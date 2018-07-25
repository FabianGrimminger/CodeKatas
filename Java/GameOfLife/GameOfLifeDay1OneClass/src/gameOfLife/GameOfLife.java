package gameOfLife;

import java.util.Random;

public class GameOfLife {
	
	private Random random;
	private static int width = 10;
	private static int height = 10;
	private boolean[][] gamefield = new boolean[width][height];
	
	public GameOfLife(String string) {
		random = null;
	}

	public GameOfLife(int seed) {
		random = new Random(seed);
	}

	public GameOfLife() {
		random = new Random();
	}
	
	private void Run() {
		createGameField();
		
		for(int i=0;i<10;i++) {
			printOutGameField();
			envolutionStep();
		}
		System.out.println("----------Finish----------");		
	}

	
	private void envolutionStep() {
		boolean[][] temp = new boolean[width][height];
		for(int x = 0;x<width;x++) {
			for(int y = 0;y<height;y++) {
				int n = countNeighbours(x,y, gamefield[x][y]);
				temp[x][y] = isAlive(n, gamefield[x][y]);
			}
		}
		gamefield = temp;		
	}

	private boolean isAlive(int n, boolean self) {
		if(n==3) {
			return true;
		}
		if(n==2 && self) {
			return true;
		}
		return false;
	}

	private int countNeighbours(int x, int y, boolean self) {
		int counter = 0;
		for(int i=-1;i<=1;i++) {
			for(int j=-1;j<=1;j++) {
				int tempX = (x + i)%width;
				int tempY = (y + j)%height;
				if(tempX <0) {
					tempX = width + tempX;
				}
				if(tempY < 0) {
					tempY = height + tempY;
				}
				
				if(gamefield[tempX][tempY]) {
					counter++;
				}
			}
		}
		if(self) {
			counter--;
		}
		return counter;
	}

	private void printOutGameField() {
		for(boolean[] line : gamefield) {
			for(boolean cell : line) {
				if(cell) {
					System.out.print(" x");
				}else {
					System.out.print(" o");
				}
			}
			System.out.println();
		}		
		System.out.println();
	}

	private void createGameField() {
		if(random!=null) {
			int counter = 0;
			while(counter <20) {
				int x = random.nextInt(width);
				int y = random.nextInt(height);
				if(!gamefield[x][y]) {
					counter++;
					gamefield[x][y]=true;
				}
			}
		}else {
			gamefield[0][1] = true;
			gamefield[1][2] = true;
			gamefield[2][0] = true;
			gamefield[2][1] = true;
			gamefield[2][2] = true;
		}
		
	}

	public static void main(String[] args) {
		
		if(args.length == 0) {
			new GameOfLife().Run();
		}
		if(args.length > 0 && args[0].equals("g")) {
			new GameOfLife("g").Run();
		}
		if(args.length>0) {
			try {
				int seed = Integer.parseInt(args[0]);
				new GameOfLife(seed).Run();
			}catch(Exception e) {
				System.out.println("Wrong params: use none, g or a number");
			}
		}	
	}
}
