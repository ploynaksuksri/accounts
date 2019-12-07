using accounts.core.Dto;
using accounts.DAL.repositories;
using System;
using System.Collections.Generic;
using System.Text;
using accounts.DAL.models;

namespace accounts.core
{
    public class TransactionManager : ITransactionManager
    {
        private IFeeCalculator _feeCalculator;
        private IRepository<Transaction> _transactionRepo;

        public TransactionManager(IFeeCalculator feeCalculator,
            IRepository<Transaction> transactionRepo)
        {
            _feeCalculator = feeCalculator;
            _transactionRepo = transactionRepo;
        }

        public Transaction Deposit(AccountDepositDto depositDto)
        {
            var fee = _feeCalculator.CalculateDepositFee(depositDto.Amount);
            Transaction transaction = new Transaction
            {
                TransactionType = TransactionTypeEnum.Deposit,
                AccountId = depositDto.AccountId,
                OriginalAmount = depositDto.Amount,
                NetAmount = depositDto.Amount - fee,
                Fee = fee
            };

            _transactionRepo.Add(transaction);
            return transaction;
        }


        public Transaction Transfer(AccountTransferDto transferDto)
        {
            return ProcessTransfer(transferDto, TransactionTypeEnum.Transfer);
        }

        public Transaction Receive(AccountTransferDto transferDto)
        {
            return ProcessTransfer(transferDto, TransactionTypeEnum.Receive);
        }

        private Transaction ProcessTransfer(AccountTransferDto transferDto, TransactionTypeEnum type)
        {
            var transaction = new Transaction
            {
                TransactionType = type,
                OriginalAmount = transferDto.Amount,
                NetAmount = transferDto.Amount,
                Fee = 0.0m
            };
            
            if (type == TransactionTypeEnum.Transfer)
            {
                transaction.AccountId = transferDto.FromAccount.Id;
                transaction.AssociatedAccountId = transferDto.ToAccount.Id;
            }
            else
            {
                transaction.AccountId = transferDto.ToAccount.Id;
                transaction.AssociatedAccountId = transferDto.FromAccount.Id;
            }
            _transactionRepo.Add(transaction);
            return transaction;
        }
   
 

    }

    public interface ITransactionManager
    {
        Transaction Deposit(AccountDepositDto depositDto);
        Transaction Transfer(AccountTransferDto transferDto);
        Transaction Receive(AccountTransferDto transferDto);
    }
}
