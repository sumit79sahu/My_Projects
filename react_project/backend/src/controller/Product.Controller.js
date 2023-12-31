const product =require('../model/Product')
const category =require('../model/Category')

const getProducts=
{
    path:'/api/products',
    method:'get',
    handler:async(req,res)=>
    {
        try {
            const result= await product.findAll({include:category});
            res.json(result)  ;
        } catch (error) {
             res.sendStatus(404);
        }
    }
}

const getProductById=
{
    path:'/api/product/:id',
    method:'get',
    handler:async(req,res)=>
    {
        try {
            const result= await product.findAll({include:{model:category},where:{productId:req.params.id}});
            res.json(result)  ;
        } catch (error) {
            console.log(error)
             res.sendStatus(404);
        }
    }
}
const getProductByCategory=
{
    path:'/api/product/category/:id',
    method:'get',
    handler:async(req,res)=>
    {
        try {
            // console.log(req.params.id)
            const result= await product.findAll({where:{categoryId:req.params.id}});
            console.log(result)
            res.json(result)  ;
        } catch (error) {
            console.log(error)
             res.sendStatus(404);
        }
    }
}


module.exports={getProducts,getProductById,getProductByCategory}