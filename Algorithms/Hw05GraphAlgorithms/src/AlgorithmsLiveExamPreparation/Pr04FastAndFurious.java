package AlgorithmsLiveExamPreparation;

import java.util.*;

public class Pr04FastAndFurious {
    static Double[][] graph;
    final static HashMap<String, Integer> cityMap = new HashMap<>();
    static int cityIndex;
    final static HashMap<String, List<Car>> records = new HashMap<>();
    final static TreeSet<String> speeders = new TreeSet<>();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Queue<Integer> asd = new ArrayDeque<>();
        asd.add(5);
        sc.nextLine();
        cityIndex = 0;
        graph = new Double[2500][2500];
        while (true) {
            String[] intput = sc.nextLine().split(" ");
            if (intput[0].equals("Records:")) {
                break;
            }
            String city1 = intput[0];
            String city2 = intput[1];
            double dist = Double.parseDouble(intput[2]);
            double speed = Double.parseDouble(intput[3]);
            double time = dist / speed;
            if(!cityMap.containsKey(city1)) {
                cityMap.put(city1, cityIndex);
                cityIndex++;
            }
            if(!cityMap.containsKey(city2)) {
                cityMap.put(city2, cityIndex);
                cityIndex++;
            }
            graph[cityMap.get(city1)][cityMap.get(city2)] = time;
            graph[cityMap.get(city2)][cityMap.get(city1)] = time;
            graph[cityMap.get(city2)][cityMap.get(city2)] = 0d;
            graph[cityMap.get(city1)][cityMap.get(city1)] = 0d;
        }

        findAllShortestPaths();
        while (true) {
            String[] intput = sc.nextLine().split(" ");
            if (intput[0].equals("End")) {
                break;
            }
            String location = intput[0];
            String id = intput[1];
            String[] timeStr = intput[2].split(":");
            double h = Double.parseDouble(timeStr[0]);
            double m = Double.parseDouble(timeStr[1]);
            double s = Double.parseDouble(timeStr[2]);
            double time = h + m / 60d + s / 3600d; // ???
            if (!records.containsKey(id)) {
                Car car = new Car(location, time);
                records.put(id, new ArrayList<>());
                records.get(id).add(car);
                continue;
            }
            for (Car carRec : records.get(id)) {
                String lastLocation = carRec.lastLocation;
                double lastTime = carRec.lastTime;
                double timeDiff = Math.abs(lastTime - time);
                double minTime = graph[cityMap.get(location)][cityMap.get(lastLocation)];
                if (!Double.isInfinite(minTime)) {
                    boolean isSpeeding = Double.compare(minTime, timeDiff) > 0;
                    if(isSpeeding) {
                        speeders.add(id);
                    }
                }
            }
            Car currCar = new Car(location, time);
            records.get(id).add(currCar);
        }

        speeders.forEach(System.out::println);
    }

    private static void findAllShortestPaths() {
        for (int k = 0; k < cityIndex; k++) {
            for (int i = 0; i < cityIndex; i++) {
                for (int j = 0; j < cityIndex; j++) {
                    if (graph[i][j] == null) {
                        graph[i][j] = Double.POSITIVE_INFINITY;
                    }
                    if (graph[i][k] + graph[k][j] < graph[i][j]) {
                        graph[i][j] = graph[i][k] + graph[k][j];
                    }
                }
            }
        }
    }
}

class Car {
    public String lastLocation;
    public double lastTime;

    public Car(String lastLocation, double lastTime) {
        this.lastLocation = lastLocation;
        this.lastTime = lastTime;
    }
}
