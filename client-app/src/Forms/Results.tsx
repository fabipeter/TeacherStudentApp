import React from "react";
import { Button, Label } from "semantic-ui-react";
import MyTextInput from "../components/form/MyTextInput";
import { Link } from "react-router-dom";
interface IProps {
  results: {
    nationalIdNumber: string;
    name: string;
    surname: string;
    dateOfBirth: string;
    teacherNumber: string;
    studentNumber: string;
    title: string;
    salary: string;
    isTeacher: boolean;
  };
}
const Results = (props: IProps) => {
  const { results } = props;
  return (
    <div className="formWrapper">
      <div className="detailsform">
        <div>
          {results.isTeacher ? (
            <Label>Teacher Number:</Label>
          ) : (
            <Label>Student Number:</Label>
          )}
          {results.teacherNumber}
          {results.studentNumber}
          <Label>National Id Number:</Label>
          {results.nationalIdNumber}
          <Label>Name:</Label>
          {results.name}
          <Label>Surname:</Label>
          {results.surname}
          {results.isTeacher && <Label>Title:</Label>}
          {results.isTeacher && results.title}
          {results.isTeacher && <Label>Salary:</Label>}
          {results.isTeacher && results.salary && results.salary}
          <Label>Date Of Birth</Label>
          {results.dateOfBirth}
          {/* <Button
            as={Link}
            to={"/"}
            floated="right"
            positive
            type="submit"
            content="Continue"
          /> */}
        </div>
      </div>
    </div>
  );
};

export default Results;
