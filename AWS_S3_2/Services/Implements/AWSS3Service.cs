using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using AWS_S3_2.Models.Responses;
using AWS_S3_2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_S3_2.Services.Implements
{
    public enum DocumentType
    {
        Booking = 1,
        ChargeBehalf = 2,
        Maintenance = 3
    }
    public class AWSS3Service : IAWSS3Service
    {
        public readonly AmazonS3Client _client;
        public readonly string profile = "default";
        public AWSS3Service()
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials(profile, out awsCredentials))
            {
                // Use awsCredentials to create an Amazon S3 service client
                var client = new AmazonS3Client(awsCredentials);
                _client = client;
            }
        }
        public async Task<int> getNumberOfBucketsAsync()
        {
            var response = await _client.ListBucketsAsync();
            return response.Buckets.Count();
        }

        //public async Task<PutBucketResponse> CreateBucketAsync(string bucketName)
        //{
        //    var response = await _client.PutBucketAsync(bucketName);
        //    return response;
        //}

        public async Task<AWSS3ObjectResponse> getObjectAsync(string fileKey, int type)
        {
            string stringType = ((DocumentType)type).ToString();

            var request = new GetObjectRequest()
            {
                BucketName = "etms-test/"+stringType,
                Key = fileKey,
            };

            GetObjectResponse response = await _client.GetObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                AWSS3ObjectResponse ObjResult = new AWSS3ObjectResponse();
                ObjResult.Result = response.ResponseStream;
                ObjResult.Extenstion = response.Headers["Content-Type"];

                return ObjResult;
            }
            else
                return null;
        }

        public async Task<PutObjectResponse> postObjectAsync(IFormFile file, int type)
        {
            string stringType = ((DocumentType)type).ToString();

            var putRequest = new PutObjectRequest()
            {
                BucketName = "etms-test/" + stringType,
                Key = file.FileName, //change key = identity key => database
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };
            var result = await _client.PutObjectAsync(putRequest);

            if(result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
