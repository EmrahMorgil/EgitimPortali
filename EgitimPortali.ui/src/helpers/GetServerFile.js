const getServerFile = (fileName) => {
    return process.env.REACT_APP_API_URI + `/docs/${fileName}`;
  };
  export default getServerFile;
  