import React from "react";
import { useNavigate } from "react-router-dom";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";

const HomePage = () => {
  const navigate = useNavigate();
  return (
    <Segment inverted textAlign="center" vertical className="masthead">
      <Container text>
        <Header as="h1" inverted>
          {/* <Image
            size="massive"
            src="/assets/logo.png"
            alt="logo"
            style={{ marginBottom: 12 }}
          /> */}
          Teacher Student App
        </Header>
        <>
          <Button onClick={() => navigate("/student")} size="huge" inverted>
            Student
          </Button>
          <Button onClick={() => navigate("/teacher")} size="huge" inverted>
            Teacher
          </Button>
        </>
      </Container>
    </Segment>
  );
};

export default HomePage;
