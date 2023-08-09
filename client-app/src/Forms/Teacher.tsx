import React, { useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import MyTextInput from "../components/form/MyTextInput";
import { Button, Label } from "semantic-ui-react";
import { Link } from "react-router-dom";
import MySelectInput from "../components/form/MySelectInput";
import agent from "../api/agent";
import Loading from "../components/general/Loading";
import Results from "./Results";
import { toast } from "react-toastify";

const Teacher = () => {
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState("");
  const [results, setResults] = useState({
    nationalIdNumber: "",
    name: "",
    surname: "",
    dateOfBirth: "",
    teacherNumber: "",
    studentNumber: "",
    title: "",
    salary: "",
    isTeacher: true,
  });
  const [errors, setErrors] = useState<any>();
  const titleOptions = [
    { text: "Mr", value: "Mr" },
    { text: "Mrs", value: "Mrs" },
    { text: "Miss", value: "Miss" },
    { text: "Dr", value: "Dr" },
    { text: "Prof", value: "Prof" },
  ];
  const initialValues = {
    nationalIdNumber: "",
    name: "",
    surname: "",
    dateOfBirth: "",
    teacherNumber: "",
    studentNumber: "",
    title: "",
    salary: "",
    isTeacher: true,
  };
  const validationSchema = Yup.object({
    nationalIdNumber: Yup.string()
      .required("The event title is required")
      .length(11, "National Id Number must be 11 characters"),
    name: Yup.string().required("The event category is required"),
    surname: Yup.string().required(),
    title: Yup.string().required(),
    dateOfBirth: Yup.string().required("Date is required").nullable(),
    teacherNumber: Yup.string()
      .required()
      .length(10, "Teacher Number must be 10 characters"),
  });

  const handleFormSubmit = async (values: any) => {
    var isoDateString = new Date(values.dateOfBirth).toISOString();
    values.dateOfBirth = isoDateString;
    setErrors(null);
    setLoading(true);
    console.log(values);
    try {
      const response = await agent.Account.register(values);

      if (response.isSuccess) {
        toast.success("Details submitted");
        setSuccess("true");
        setResults(response.data);
        setLoading(false);
      } else {
        setSuccess("false");
        setLoading(false);
      }
    } catch (error) {
      toast.error(String(error));
      console.log(error);
      setErrors(error);
      setLoading(false);
      throw error;
    }
  };

//   console.log(success);
  return (
    <div>
      {loading && <Loading />}
      {success === "true" && <Results results={results} />}
      <div className="formWrapper">
        <Formik
          enableReinitialize
          validationSchema={validationSchema}
          initialValues={initialValues}
          onSubmit={(values) => handleFormSubmit(values)}
        >
          {({ handleSubmit, isValid, isSubmitting, dirty }) => (
            <Form
              className="ui form detailsform"
              onSubmit={handleSubmit}
              autoComplete="off"
            >
              <Label>Teacher Number</Label>
              <MyTextInput name="teacherNumber" placeholder="e.g 1234567890" />
              <Label>National Id Number</Label>
              <MyTextInput
                name="nationalIdNumber"
                placeholder="e.g 93939903038"
                // type="number"
              />
              <Label>Name</Label>
              <MyTextInput name="name" placeholder="Enter Name" />
              <Label>Surname</Label>
              <MyTextInput name="surname" placeholder="Enter Surname" />
              <Label>Title</Label>

              <MySelectInput
                options={titleOptions}
                name="title"
                placeholder="Choose Title"
              />
              <Label>Salary(optional)</Label>
              <MyTextInput name="salary" placeholder="Enter Salary" />
              <Label>Date Of Birth</Label>
              <MyTextInput
                name="dateOfBirth"
                placeholder="Date Of Birth"
                type="date"
              />
              <Button
                disabled={isSubmitting || !dirty || !isValid}
                loading={isSubmitting}
                floated="right"
                positive
                type="submit"
                content="Submit"
              />
              <Button
                as={Link}
                to="/"
                floated="right"
                type="button"
                content="Cancel"
              />
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default Teacher;
