const GetAsync = async (path, req) => {
    try {
      const response = await fetch(`${process.env.REACT_APP_API_URI}/api/${path}`, req);
      const data = await response.json();
      return data;
    } catch (error) {
      return error;
    }
  };
  
  const PostAsync = async (path, req) => {
    try {
      const response = await fetch(`${process.env.REACT_APP_API_URI}/api/${path}`, req);
      const data = await response.json();
      return data;
    } catch (error) {
      return error;
    }
  };
  
  export { GetAsync, PostAsync };
  