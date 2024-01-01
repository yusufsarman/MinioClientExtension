# MinioClientExtension

Minio client for .net Core project.

## How to Use
add this to appsettings.json. 
```
"Minio": {
  "EndPoint": "your-domain.com:9000",
  "AccessKey": "gmyXGasd8bY91QEasd74",
  "SecretKey": "Y5JasdKK0SEqlmvcasdT4sVZIfD9K2asd105OWT6",
  "IsSSL": false
}
```

Make sure to change with your informations.

Add reference to MinioExtension class library.
 ```
services.AddMinioService(options =>
 {
     options.EndPoint = configuration["EndPoint"];
     options.AccessKey = configuration["AccessKey"];
     options.SecretKey = configuration["SecretKey"];
     options.IsSSL = Convert.ToBoolean(configuration["IsSSL"]);                
 });
```
 register service.

 now only need to dependency inject the IMinioService in your class

 
Also included Client example project.You can check the usage there

