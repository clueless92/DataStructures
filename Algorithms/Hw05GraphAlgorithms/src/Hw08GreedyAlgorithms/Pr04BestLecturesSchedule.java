package Hw08GreedyAlgorithms;

import java.util.ArrayList;
import java.util.Scanner;

public class Pr04BestLecturesSchedule {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        ArrayList<Lecture> lectures = new ArrayList<>();
        int count = Integer.parseInt(sc.nextLine().split(" ")[1]);
        for (int i = 0; i < count; i++) {
            String[] input = sc.nextLine().split("\\W+");
            String name = input[0];
            int start = Integer.parseInt(input[1]);
            int finish = Integer.parseInt(input[2]);
            lectures.add(new Lecture(start, finish, name));
        }
        ArrayList<Lecture> outputLectures = new ArrayList<>();
        while (lectures.size() > 0) {
            lectures.sort((lec1, lec2) -> Integer.compare(lec1.finish, lec2.finish));
            Lecture currLecture = lectures.get(0);
            outputLectures.add(currLecture);
            ArrayList<Lecture> lecturesToRemove = new ArrayList<>();
            for (Lecture lecture : lectures) {
                if (lecture.start < currLecture.finish) {
                    lecturesToRemove.add(lecture);
                }
            }
            lectures.removeAll(lecturesToRemove);
        }
        System.out.printf("Lectures (%d):%n", outputLectures.size());
        for (Lecture lecture : outputLectures) {
            System.out.println(lecture);
        }
    }

    private static class Lecture {
        public int start;
        public int finish;
        public String name;

        public Lecture(int start, int finish, String name) {
            this.start = start;
            this.finish = finish;
            this.name = name;
        }

        @Override
        public String toString() {
            return String.format("%d-%d -> %s", this.start, this.finish, this.name);
        }
    }
}

