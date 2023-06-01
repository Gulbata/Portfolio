package subject;

public enum Subject {
    LITERATURE(SubjectType.HUMANITY,4),
    HISTORY   (SubjectType.HUMANITY,5),
    PHYSICS   (SubjectType.SCIENCE, 6),
    CHEMISTRY (SubjectType.SCIENCE, 7);

    private SubjectType subjectTypes;
    private int startingYear;

    Subject(SubjectType subjectTypes, int startingYear) {
        this.subjectTypes = subjectTypes;
        this.startingYear = startingYear;
    }

    public SubjectType getSubjectTypes() {
        return subjectTypes;
    }

    public int getStartingYear() {
        return startingYear;
    }

    public void setSubjectTypes(SubjectType subjectTypes) {
        this.subjectTypes = subjectTypes;
    }

    public void setStartingYear(int startingYear) {
        this.startingYear = startingYear;
    }
}
